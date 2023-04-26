using Cinema.WEB.Models.PersonModels.PersonVms;

namespace Cinema.WEB.Models.PersonModels
{
    public class PersonsFilteredResponse
    {
        public string? Search { get; set; }

        public bool Sort { get; set; }

        public PageResponse<PersonVm> PersonsPage { get; set; } = new PageResponse<PersonVm>();
    }
}
