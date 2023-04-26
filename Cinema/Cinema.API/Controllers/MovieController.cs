using AutoMapper;
using Cinema.API.Models.MovieModels.MovieDtos;
using Cinema.API.Models.MovieModels;
using Cinema.API.Models;
using Cinema.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IApiResponseFactory _response;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IApiResponseFactory response, IMapper mapper)
        {
            _movieService = movieService;
            _response = response;
            _mapper = mapper;
        }

        //узнал что такое идемпотентность поэтому решил изменить на Get)
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetFiltereMoviesAsync([FromQuery] string? search = null,
                                                             [FromQuery] string? titleFilter = null,
                                                             [FromQuery] string? DirectorFilter = null,
                                                             [FromQuery] string? GenreFilter = null,
                                                             [FromQuery] bool sort = true,
                                                             [FromQuery] int page = 1,
                                                             [FromQuery] int pageSize = 50)
        {
            try
            {
                if (pageSize < 1 || page < 1)
                    return _response.BadRequestResponse(new List<string> { "Страница или размер страницы не может быть меньше 1." });
                var opt = new MoviesFilterRequest(search, titleFilter, DirectorFilter, GenreFilter, sort, pageSize, page);
                var pageOfMovies = await _movieService.GetFilteredPageOfMoviesAsync(opt);
                var filteredResponse = _mapper.Map<MoviesFilteredResponse>(opt);
                filteredResponse.MoviesPage = pageOfMovies;
                return _response.SuccessResponse(filteredResponse);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> CreateMovieAsync([FromBody] MovieCreateDto movieDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return _response.BadRequestResponse(ModelState.Values.SelectMany(
                    v => v.Errors.Select(e => e.ErrorMessage)).ToList());
                await _movieService.CreateMovieAsync(movieDto);
                return _response.CreatedResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetMovieByIdAsync(Guid id)
        {
            try
            {
                var movieDto = await _movieService.GetMovieByIdAsync(id);
                return _response.SuccessResponse(movieDto);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> UpdateMovieAsync([FromBody] MovieUpdateDto movieDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return _response.BadRequestResponse(ModelState.Values.SelectMany(
                    v => v.Errors.Select(e => e.ErrorMessage)).ToList());
                await _movieService.UpdateMovieAsync(movieDto);
                return _response.SuccessResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> DeleteMovieAsync(Guid id)
        {
            try
            {
                await _movieService.DeleteMovieByIdAsync(id);
                return _response.NoContentResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }
    }
}
