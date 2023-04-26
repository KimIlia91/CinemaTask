using Cinema.WEB.Helpers;
using Cinema.WEB.Models;
using Cinema.WEB.Models.PersonModels;
using Cinema.WEB.Models.PersonModels.PersonDtos;
using Cinema.WEB.Services.IServices;
using Newtonsoft.Json;

namespace Cinema.WEB.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly string _cinemaUrl;

        public PersonService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _cinemaUrl = configuration.GetValue<string>("ServiceUrl:CinemaApi")!;
        }

        public async Task<bool> CreatePersonAsync(PersonCreateDto dto, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = _cinemaUrl + "/api/person",
                Data = dto,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<bool> DeletePersonAsync(Guid id, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = _cinemaUrl + "/api/person/" + id,
                Token = token
            });

            return response.IsSuccess;
        }

        public async Task<PersonsFilteredResponse> GetAllPersonsAsync(string token, PersonsFilterRequest request)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_cinemaUrl}/api/person?search={request.Search}&sort={request.Sort}&page={request.Page}&pageSize={request.PageSize}",
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var page = JsonConvert.DeserializeObject<PersonsFilteredResponse>(Convert.ToString(response.Result)!);
                return page!;
            }

            return new PersonsFilteredResponse();
        }

        public async Task<PersonDto> GetPersonAsync(Guid? id, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _cinemaUrl + "/api/person/" + id,
                Token = token
            });

            if (response.Result != null && response.IsSuccess)
            {
                var personDto = JsonConvert.DeserializeObject<PersonDto>(Convert.ToString(response.Result)!);
                return personDto ?? new PersonDto();
            }

            return new PersonDto();
        }

        public async Task<bool> UpdatePersonAsync(PersonDto dto, string token)
        {
            var response = await SendAsync(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = _cinemaUrl + "/api/person",
                Data = dto,
                Token = token
            });

            return response.IsSuccess;
        }
    }
}
