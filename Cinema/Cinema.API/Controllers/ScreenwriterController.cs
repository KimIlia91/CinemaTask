using Cinema.API.Models;
using Cinema.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ScreenwriterController : ControllerBase
    {
        private readonly IScreenwriterService _screenwriterService;
        private readonly IApiResponseFactory _response;

        public ScreenwriterController(IScreenwriterService screenwriterService, IApiResponseFactory response)
        {
            _screenwriterService = screenwriterService;
            _response = response;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetScreenwritersAsync()
        {
            try
            {
                var screenwriterDtos = await _screenwriterService.GetScreenwritersAsync();
                return _response.SuccessResponse(screenwriterDtos);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetScreenwriterByIdAsync(Guid id)
        {
            try
            {
                var screenwriterDto = await _screenwriterService.GetScreenwriterByIdAsync(id);
                return _response.SuccessResponse(screenwriterDto);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }
    }
}
