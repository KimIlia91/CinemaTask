using Cinema.API.Models.PersonModels.PersonDtos;

namespace Cinema.API.Models.PersonModels
{
    public class PersonsFilteredResponse
    {
        public string? Search { get; set; }

        public bool Sort { get; set; }

        public Page<PersonDto> PersonsPage { get; set; } = null!;
    }
}
