using Cinema.API.Models.CastModels.CastModelDtos;

namespace Cinema.API.Services.IServices
{
    public interface IDirectorService
    {
        Task<IEnumerable<DirectorDto>> GetDirectorsAsync();

        Task<DirectorDto> GetDirectorByIdAsync(Guid id);
    }
}
