using Cinema.API.Models;
using Cinema.API.Services.IServices;
using System.Net;

namespace Cinema.API.Services
{
    public class ApiResponseFactory : IApiResponseFactory
    {
        /// <inheritdoc/>
        public ApiResponse BadRequestResponse(List<string> errors)
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                ErrorMessage = errors
            };
        }

        /// <inheritdoc/>
        public ApiResponse ErrorResponse(List<string> errorMessages)
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessage = errorMessages
            };
        }

        /// <inheritdoc/>
        public ApiResponse NotFoundResponse(List<string> errorMessages)
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
                ErrorMessage = errorMessages ?? throw new ArgumentNullException(nameof(errorMessages), "The error messages cannot be null.")
            };
        }

        /// <inheritdoc/>
        public ApiResponse SuccessResponse(object? result = null)
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            };
        }

        /// <inheritdoc/>
        public ApiResponse NoContentResponse()
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.NoContent,
                ErrorMessage = new List<string>(),
                IsSuccess = true
            };
        }

        /// <inheritdoc/>
        public ApiResponse CreatedResponse(object? result = null)
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.Created,
                IsSuccess = true,
                Result = result
            };
        }
    }
}
