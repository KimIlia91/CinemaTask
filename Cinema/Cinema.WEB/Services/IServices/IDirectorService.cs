using Cinema.WEB.Models.CastModels.CastDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.WEB.Services.IServices
{
    public interface IDirectorService
    {
        Task<IEnumerable<SelectListItem>> GetSelectListOfDirectorsAsync(string token, IEnumerable<Guid> selectedDirectorIds);

        Task<DirectorDto> GetDirectorAsync(Guid? id, string token);
    }
}
