using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Models.CastModels.CastDtos;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly ICinemaApiHttpClientService _cinemaApi;

        public DirectorService(ICinemaApiHttpClientService cinemaApi)
        {
            _cinemaApi = cinemaApi;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListOfDirectorsAsync(
            string token,
            IEnumerable<Guid> selectedDirectorIds)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/director",
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var dtos = JsonConvert.DeserializeObject<List<DirectorDto>>(Convert.ToString(response.Result)!);
                var selectListItems = dtos!.Select(d =>
                new SelectListItem { Text = $"{d.FirstName} {d.LastName}", Value = d.Id.ToString() }).ToList();

                if (!selectedDirectorIds.Any())
                    return selectListItems;

                foreach (var selectedItem in selectListItems.Where(item =>
                    selectedDirectorIds.Contains(Guid.Parse(item.Value))))
                {
                    selectedItem.Selected = true;
                }

                return selectListItems.OrderByDescending(a => a.Selected);
            }

            return Enumerable.Empty<SelectListItem>();
        }

        public async Task<DirectorDto> GetDirectorAsync(Guid? id, string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/director/" + id,
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var directorDto = JsonConvert.DeserializeObject<DirectorDto>(Convert.ToString(response.Result)!);
                return directorDto ?? new DirectorDto();
            }

            return new DirectorDto();
        }
    }
}
