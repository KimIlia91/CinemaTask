using Cinema.API.Models.CastModels.CastModelDtos;

namespace Cinema.API.Services.IServices
{
    public interface IScreenwriterService
    {
        Task<IEnumerable<ScreenwriterDto>> GetScreenwritersAsync();

        Task<ScreenwriterDto> GetScreenwriterByIdAsync(Guid id);
    }
}
