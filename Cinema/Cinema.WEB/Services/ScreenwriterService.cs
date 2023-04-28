using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Models.CastModels.CastDtos;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class ScreenwriterService : IScreenwriterService
    {
        private readonly ICinemaApiHttpClientService _cinemaApi;

        public ScreenwriterService(ICinemaApiHttpClientService cinemaApi)
        {
            _cinemaApi = cinemaApi;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListOfScreenwritersAsync(
            string token,
            IEnumerable<Guid> selectedScreenwriterIds)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/screenwriter",
                Token = token
            });

            if (response.Result is null && !response.IsSuccess) return Enumerable.Empty<SelectListItem>();

            var dtos = JsonConvert.DeserializeObject<List<DirectorDto>>(Convert.ToString(response.Result)!);
            var selectListItems = dtos!.Select(d =>
                new SelectListItem { Text = $"{d.FirstName} {d.LastName}", Value = d.Id.ToString() }).ToList();
            if (!selectedScreenwriterIds.Any())
                return selectListItems;

            foreach (var selectedItem in selectListItems.Where(item =>
                selectedScreenwriterIds.Contains(Guid.Parse(item.Value))))
            {
                selectedItem.Selected = true;
            }

            return selectListItems.OrderByDescending(a => a.Selected);
        }

        public async Task<ScreenwriterDto> GetScreenwriterAsync(Guid? id, string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/screenwriter/" + id,
                Token = token
            });

            if (response.Result is null && !response.IsSuccess) return new ScreenwriterDto();

            var dto = JsonConvert.DeserializeObject<ScreenwriterDto>(Convert.ToString(response.Result)!);
            return dto ?? new ScreenwriterDto();
        }
    }
}
