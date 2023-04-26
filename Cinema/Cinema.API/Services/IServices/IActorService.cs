using Cinema.API.Models.CastModels.CastModelDtos;

namespace Cinema.API.Services.IServices
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDto>> GetAllActorsAsync();

        Task<ActorDto> GetActorByIdAsync(Guid id);
    }
}
