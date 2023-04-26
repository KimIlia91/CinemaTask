namespace Cinema.WEB.Models.PersonModels
{
    public class PersonsFilterRequest
    {
        public string? Search { get; set; }

        public bool Sort { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public PersonsFilterRequest(bool sort, string? search = null, int pageSize = 50, int page = 1)
        {
            Search = search;
            Page = page;
            Sort = sort;
            PageSize = pageSize;
        }
    }
}
