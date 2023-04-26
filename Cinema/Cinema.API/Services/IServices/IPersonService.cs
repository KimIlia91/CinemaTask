using Cinema.API.Models.PersonModels.PersonDtos;
using Cinema.API.Models.PersonModels;
using Cinema.API.Models;

namespace Cinema.API.Services.IServices
{
    public interface IPersonService
    {
        Task<Page<PersonDto>> GetPageOfPersonsAsync(PersonsFilterRequest opt);

        Task CreatePersonAsync(PersonCreateDto personDto);

        Task<PersonDto> GetPersonByIdAsync(Guid id);

        Task UpdatePersonAsync(PersonDto personDto);

        Task DeletePersonAsync(Guid id);
    }
}
