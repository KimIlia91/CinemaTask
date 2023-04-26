using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Models.CastModels.CastDtos;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class ScreenwriterService : BaseService, IScreenwriterService
    {
        private readonly string _cinemaUrl;

        public ScreenwriterService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _cinemaUrl = configuration.GetValue<string>("ServiceUrl:CinemaApi")!;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListOfScreenwritersAsync(
            string token,
            IEnumerable<Guid> selectedScreenwriterIds)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_cinemaUrl}/api/screenwriter",
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
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

            return Enumerable.Empty<SelectListItem>();
        }

        public async Task<ScreenwriterDto> GetScreenwriterAsync(Guid? id, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _cinemaUrl + "/api/screenwriter/" + id,
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var dto = JsonConvert.DeserializeObject<ScreenwriterDto>(Convert.ToString(response.Result)!);
                return dto ?? new ScreenwriterDto();
            }

            return new ScreenwriterDto();
        }
    }
}
