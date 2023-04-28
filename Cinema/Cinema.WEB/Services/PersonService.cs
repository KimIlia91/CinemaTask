using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Models.PersonModels;
using Cinema.WEB.Models.PersonModels.PersonDtos;
using Cinema.WEB.Services.IServices;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class PersonService : IPersonService
    {
        private readonly ICinemaApiHttpClientService _cinemaApi;

        public PersonService(ICinemaApiHttpClientService cinemaApi)
        {
            _cinemaApi = cinemaApi;
        }

        public async Task<bool> CreatePersonAsync(PersonCreateDto dto, string token)
        {
            var response = await _cinemaApi.PostAsync(new ApiRequest()
            {
                Url = "/api/person",
                Data = dto,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<bool> DeletePersonAsync(Guid id, string token)
        {
            var response = await _cinemaApi.DeleteAsync(new ApiRequest()
            {
                Url = "/api/person/" + id,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<PersonsFilteredResponse> GetAllPersonsAsync(string token, PersonsFilterRequest request)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = $"/api/person?search={request.Search}&sort={request.Sort}&page={request.Page}&pageSize={request.PageSize}",
                Token = token
            });

            if (response.Result is null && !response.IsSuccess) return new PersonsFilteredResponse();

            var page = JsonConvert.DeserializeObject<PersonsFilteredResponse>(Convert.ToString(response.Result)!);
            return page!;
        }

        public async Task<PersonDto> GetPersonAsync(Guid? id, string token)
        {
            var response = await _cinemaApi.GetAsync(new ApiRequest()
            {
                Url = "/api/person/" + id,
                Token = token
            });

            if (response.Result is null && !response.IsSuccess) return new PersonDto();

            var personDto = JsonConvert.DeserializeObject<PersonDto>(Convert.ToString(response.Result)!);
            return personDto!;
        }

        public async Task<bool> UpdatePersonAsync(PersonDto dto, string token)
        {
            var response = await _cinemaApi.PutAsync(new ApiRequest()
            {
                Url = "/api/person",
                Data = dto,
                Token = token
            });

            return response.IsSuccess;
        }
    }
}
