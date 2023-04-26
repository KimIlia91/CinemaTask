using Cinema.WEB.Models.CastModels.CastDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.WEB.Services.IServices
{
    public interface IActorService
    {
        Task<IEnumerable<SelectListItem>> GetSelectListOfActorsAsync(string token, IEnumerable<Guid> selectedActorIds);

        Task<ActorDto> GetActorAsync(Guid? id, string token);
    }
}
