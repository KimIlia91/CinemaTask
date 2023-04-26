using Cinema.API.Models.GenreModels.GenreDtos;
using Cinema.API.Models;
using Cinema.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IApiResponseFactory _response;

        /// <summary>
        /// Конструктор контроллера жанров.
        /// </summary>
        /// <param name="genreService">Сервис, предоставляющий методы для работы с жанрами.</param>
        /// <param name="response">Фабрика объектов ApiResponse, предоставляющая методы для создания ответов API.</param>
        public GenreController(IGenreService genreService, IApiResponseFactory response)
        {
            _genreService = genreService;
            _response = response;
        }

        /// <summary>
        /// Получает все жанры асинхронно.
        /// </summary>
        /// <returns>Ответ API с коллекцией объектов жанров или сообщением об ошибке.</returns>
        /// <response code="200">Успешный запрос. Возвращает коллекцию объектов жанров.</response>
        /// <response code="500">Внутренняя ошибка сервера. Возвращает сообщение об ошибке.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetAllGenresAsync()
        {
            try
            {
                var genreDtos = await _genreService.GetAllGenresAsync();
                return _response.SuccessResponse(genreDtos);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        // <summary>
        // Создает новый жанр.
        // </summary>
        // <param name="genreCreateDto">DTO для создания жанра.</param>
        // <returns>Ответ API.</returns>
        // <response code="201">Успешный ответ с кодом 201 Created.</response>
        // <response code="400">Ошибка валидации модели. Возвращается BadRequestResponse с ошибками.</response>
        // <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> CreateGenreAsync([FromBody] GenreCreateDto genreCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return _response.BadRequestResponse(ModelState.Values.SelectMany(
                                v => v.Errors.Select(e => e.ErrorMessage)).ToList());
                await _genreService.CreateGenreAsync(genreCreateDto);
                return _response.CreatedResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        // <summary>
        // Получает жанр по идентификатору.
        // </summary>
        // <param name="id">Идентификатор жанра.</param>
        // <returns>Ответ API.</returns>
        // <response code="200">Успешный ответ с кодом 200 OK и данными о жанре.</response>
        // <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetGenreByIdAsync(Guid id)
        {
            try
            {
                var genreDto = await _genreService.GetGenreByIdAsync(id);
                return _response.SuccessResponse(genreDto);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        // <summary>
        // Обновляет данные жанра.
        // </summary>
        // <param name="genreDto">DTO жанра для обновления.</param>
        // <returns>Ответ API.</returns>
        // <response code="200">Успешный ответ с кодом 200 OK.</response>
        // <response code="400">Ошибка валидации модели. Возвращается BadRequestResponse с ошибками.</response>
        // <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> UpdateGenreAsync([FromBody] GenreDto genreDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return _response.BadRequestResponse(ModelState.Values.SelectMany(
                                v => v.Errors.Select(e => e.ErrorMessage)).ToList());
                await _genreService.UpdateGenreAsync(genreDto);
                return _response.SuccessResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        // <summary>
        // Удвляет жанр по идентификатору.
        // </summary>
        // <param name="id">Идентификатор жанра.</param>
        // <returns>Ответ API.</returns>
        // <response code="204">Успешный ответ с кодом 204 жанр удален.</response>
        // <response code="500">Внутренняя ошибка сервера. Возвращается ErrorResponse с сообщением об ошибке.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> DeleteGenreAsync(Guid id)
        {
            try
            {
                await _genreService.DeleteGenreByIdAsync(id);
                return _response.NoContentResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }
    }
}
