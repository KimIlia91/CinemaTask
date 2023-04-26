using Azure;

namespace Cinema.API.Models
{
    public class Page<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Items { get; set; }

        public Page(IEnumerable<T> items, int pageNumber, int pageSize, int totalItems)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            Items = items;
        }

        public static Page<T> Create(IEnumerable<T> items, int pageNumber, int pageSize, int totalItems)
        {
            return new Page<T>(items, pageNumber, pageSize, totalItems);
        }
    }
}
