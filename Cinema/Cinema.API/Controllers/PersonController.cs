using AutoMapper;
using Cinema.API.Models.PersonModels.PersonDtos;
using Cinema.API.Models.PersonModels;
using Cinema.API.Models;
using Cinema.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IApiResponseFactory _response;
        private readonly IMapper _mapper;

        /// <summary>
        /// Контроллер для работы с персонами.
        /// </summary>
        /// <param name="personService">Сервис для работы с персонами.</param>
        /// <param name="response">Фабрика ответов API.</param>
        public PersonController(IPersonService personService, IApiResponseFactory response, IMapper mapper)
        {
            _personService = personService;
            _response = response;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех персон с возможностью поиска, сортировки, пагинации и настройки отслеживания сущностей в контексте базы данных.
        /// </summary>
        /// <param name="search">Строка поиска, содержащая фамилию или имя персоны.</param>
        /// <param name="sort">Флаг сортировки списка персон по фамилии (по умолчанию - true).</param>
        /// <param name="tracked">Флаг отслеживания сущностей в контексте базы данных (по умолчанию - false).</param>
        /// <param name="page">Номер страницы списка персон (по умолчанию - 1).</param>
        /// <param name="pageSize">Количество элементов на странице списка персон (по умолчанию - 100).</param>
        /// <returns>Ответ с пагинированным списком персон.</returns>
        /// <response code="200">Успешный ответ с пагинированным списком персон.</response>
        /// <response code="500">Ошибка на сервере.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetAllPersonsAsync([FromQuery] string? search = null,
                                                          [FromQuery] bool sort = true,
                                                          [FromQuery] int page = 1,
                                                          [FromQuery] int pageSize = 100)
        {
            try
            {
                if (pageSize < 1 || page < 1)
                    return _response.BadRequestResponse(new List<string> { "Страница или размер страницы не может быть меньше 1-ого." });
                var request = new PersonsFilterRequest(search, sort, pageSize, page);
                var fileredPageResponse = _mapper.Map<PersonsFilteredResponse>(request);
                fileredPageResponse.PersonsPage = await _personService.GetPageOfPersonsAsync(request);
                return _response.SuccessResponse(fileredPageResponse);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        /// <summary>
        /// Метод создания новой персоны
        /// </summary>
        /// <param name="personDto">DTO создаваемой персоны</param>
        /// <returns>Успешный ответ API или ответ с ошибкой BadRequest или InternalServerError</returns>
        /// <response code="200">Успешный ответ API</response>
        /// <response code="400">Ответ с ошибкой BadRequest</response>
        /// <response code="500">Ответ с ошибкой InternalServerError</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> CreatePersonAsync([FromBody] PersonCreateDto personDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return _response.BadRequestResponse(ModelState.Values.SelectMany(
                    v => v.Errors.Select(e => e.ErrorMessage)).ToList());
                await _personService.CreatePersonAsync(personDto);
                return _response.CreatedResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        /// <summary>
        /// Получает информацию о человеке по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор человека в формате GUID.</param>
        /// <returns>Объект ApiResponse с результатом выполнения операции.</returns>
        /// <response code="200">Успешное выполнение операции.</response>
        /// <response code="400">Некорректный формат входных данных.</response>
        /// <response code="500">Ошибка сервера при выполнении операции.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetPersonByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guidId))
                    return _response.BadRequestResponse(new List<string> { "Входные данные не являются формата Guid." });
                var personDto = await _personService.GetPersonByIdAsync(guidId);
                return _response.SuccessResponse(personDto);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        /// <summary>
        /// Обновляет информацию о человеке.
        /// </summary>
        /// <param name="personDto">DTO объект с информацией о человеке.</param>
        /// <returns>Объект ApiResponse с успешным статусом или ошибкой.</returns>
        /// <response code="200">Успешный ответ с информацией обновленного человека.</response>
        /// <response code="400">Неверный формат входных данных.</response>
        /// <response code="500">Внутренняя ошибка сервера.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> UpdatePersonAsync([FromBody] PersonDto personDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return _response.BadRequestResponse(ModelState.Values.SelectMany(
                            v => v.Errors.Select(e => e.ErrorMessage)).ToList());
                await _personService.UpdatePersonAsync(personDto);
                return _response.SuccessResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        /// <summary>
        /// Удаление персоны по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персоны.</param>
        /// <returns>Ответ API.</returns>
        /// <response code="204">Успешное удаление персоны.</response>
        /// <response code="400">Неверный формат входных данных.</response>
        /// <response code="500">Внутренняя ошибка сервера.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> UpdatePersonAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guidId))
                    return _response.BadRequestResponse(new List<string> { "Входные данные не являются формата Guid." });
                await _personService.DeletePersonAsync(guidId);
                return _response.NoContentResponse();
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }
    }
}
