using Cinema.API.Models.MovieModels;
using Cinema.API.Models.PersonModels;

namespace Cinema.API.Models.CastModels
{
    public class Screenwriter
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
