using Cinema.API.Models;
using Cinema.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;
        private readonly IApiResponseFactory _response;

        public DirectorController(IDirectorService directorService, IApiResponseFactory response)
        {
            _directorService = directorService;
            _response = response;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetdirectorsAsync()
        {
            try
            {
                var directorDtos = await _directorService.GetDirectorsAsync();
                return _response.SuccessResponse(directorDtos);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetDirectorByIdAsync(Guid id)
        {
            try
            {
                var direcotrDto = await _directorService.GetDirectorByIdAsync(id);
                return _response.SuccessResponse(direcotrDto);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }
    }
}
