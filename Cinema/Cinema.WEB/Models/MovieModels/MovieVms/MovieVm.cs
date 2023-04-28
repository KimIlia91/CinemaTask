using Cinema.WEB.Models.CastModels.CastDtos;
using Cinema.WEB.Models.GenreModels.GenreDtos;

namespace Cinema.WEB.Models.MovieModels.MovieVms
{
    public class MovieVm
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public int RuntimeInMinutes { get; set; }

        public string Country { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public GenreDto Genre { get; set; } = null!;

        public virtual IEnumerable<ActorDto> Actors { get; set; } = new List<ActorDto>();

        public virtual IEnumerable<DirectorDto> Directors { get; set; } = new List<DirectorDto>();

        public virtual IEnumerable<ScreenwriterDto> Screenwriters { get; set; } = new List<ScreenwriterDto>();
    }
}
