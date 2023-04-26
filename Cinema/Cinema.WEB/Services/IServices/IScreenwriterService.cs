using Cinema.WEB.Models.CastModels.CastDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.WEB.Services.IServices
{
    public interface IScreenwriterService
    {
        Task<IEnumerable<SelectListItem>> GetSelectListOfScreenwritersAsync(string token, IEnumerable<Guid> selectedScreenwriterIds);

        Task<ScreenwriterDto> GetScreenwriterAsync(Guid? id, string token);
    }
}
