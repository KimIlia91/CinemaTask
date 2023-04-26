namespace Cinema.API.Models.PersonModels
{
    public class PersonsFilterRequest
    {
        public string? Search { get; set; }

        public bool Sort { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public PersonsFilterRequest(string? search,
                                   bool sort,
                                   int pageSize,
                                   int page)
        {
            Search = search;
            Sort = sort;
            PageSize = pageSize;
            Page = page;
        }
    }
}
