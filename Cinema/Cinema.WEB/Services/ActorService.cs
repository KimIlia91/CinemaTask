﻿using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Models.CastModels.CastDtos;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class ActorService : IActorService
    {
        private readonly ICinemaApiHttpClientService _cinemaApi;

        public ActorService(ICinemaApiHttpClientService cinemaApi)
        {
            _cinemaApi = cinemaApi;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListOfActorsAsync(
            string token,
            IEnumerable<Guid> selectedActorIds)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/actor",
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var dtos = JsonConvert.DeserializeObject<List<ActorDto>>(Convert.ToString(response.Result)!);
                var selectListItems = dtos!.Select(d =>
                new SelectListItem { Text = $"{d.FirstName} {d.LastName}", Value = d.Id.ToString() }).ToList();

                if (!selectedActorIds.Any())
                    return selectListItems;

                foreach (var selectedItem in selectListItems.Where(item =>
                    selectedActorIds.Contains(Guid.Parse(item.Value))))
                {
                    selectedItem.Selected = true;
                }

                return selectListItems.OrderByDescending(a => a.Selected);
            }

            return Enumerable.Empty<SelectListItem>();
        }

        public async Task<ActorDto> GetActorAsync(Guid? id, string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/actor/" + id,
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var actorDto = JsonConvert.DeserializeObject<ActorDto>(Convert.ToString(response.Result)!);
                return actorDto ?? new ActorDto();
            }

            return new ActorDto();
        }
    }
}
