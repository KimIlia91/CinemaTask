namespace Cinema.WEB.Models
{
    public class PageResponse<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; } = 1;

        public int TotalPages { get; set; }

        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
