using Cinema.API.Models;
using Cinema.API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IApiResponseFactory _response;

        public ActorController(IActorService actorService, IApiResponseFactory response)
        {
            _actorService = actorService;
            _response = response;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetActorsAsync()
        {
            try
            {
                var actorDtos = await _actorService.GetAllActorsAsync();
                return _response.SuccessResponse(actorDtos);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> GetActorByIdAsync(Guid id)
        {
            try
            {
                var actorDto = await _actorService.GetActorByIdAsync(id);
                return _response.SuccessResponse(actorDto);
            }
            catch (Exception ex)
            {
                return _response.ErrorResponse(new List<string> { ex.Message });
            }
        }
    }
}
