using Cinema.WEB.Models.PersonModels;
using Cinema.WEB.Models.PersonModels.PersonDtos;

namespace Cinema.WEB.Services.IServices
{
    public interface IPersonService
    {
        Task<PersonsFilteredResponse> GetAllPersonsAsync(string token, PersonsFilterRequest request);

        Task<PersonDto> GetPersonAsync(Guid? id, string token);

        Task<bool> CreatePersonAsync(PersonCreateDto dto, string token);

        Task<bool> UpdatePersonAsync(PersonDto dto, string token);

        Task<bool> DeletePersonAsync(Guid id, string token);
    }
}
