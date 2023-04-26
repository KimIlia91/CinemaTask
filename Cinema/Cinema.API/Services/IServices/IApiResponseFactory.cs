using Cinema.API.Models;

namespace Cinema.API.Services.IServices
{
    public interface IApiResponseFactory
    {
        /// <summary>
        /// Создает объект ApiResponse с кодом ответа "BadRequest" и сообщением об ошибке.
        /// </summary>
        /// <param name="errors">Сообщение об ошибке.</param>
        /// <returns>Объект ApiResponse.</returns>
        ApiResponse BadRequestResponse(List<string> errors);

        /// <summary>
        /// Создает объект ApiResponse с кодом ответа "InternalServerError" и списком сообщений об ошибках.
        /// </summary>
        /// <param name="errorMessages">Список сообщений об ошибках.</param>
        /// <returns>Объект ApiResponse.</returns>
        ApiResponse ErrorResponse(List<string> errorMessages);

        /// <summary>
        /// Создает объект ApiResponse с кодом ответа "NotFound" и списком сообщений об ошибках.
        /// </summary>
        /// <param name="errorMessages">Список сообщений об ошибках.</param>
        /// <returns>Объект ApiResponse.</returns>
        ApiResponse NotFoundResponse(List<string> errorMessages);

        /// <summary>
        /// Создает объект ApiResponse с кодом ответа "OK" и результатом операции.
        /// </summary>
        /// <param name="result">Результат операции.</param>
        /// <returns>Объект ApiResponse.</returns>
        ApiResponse SuccessResponse(object? result = null);

        /// <summary>
        /// Создает объект ApiResponse с кодом ответа "NoContent".
        /// </summary>
        /// <returns>Объект ApiResponse.</returns>
        ApiResponse NoContentResponse();

        /// <summary>
        /// Создает объект ApiResponse с кодом ответа "NoContent".
        /// </summary>
        /// <returns>Объект ApiResponse.</returns>
        ApiResponse CreatedResponse(object? result = null);
    }
}
