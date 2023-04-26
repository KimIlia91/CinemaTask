using Cinema.API.Models.CastModels.CastModelDtos;
using Cinema.API.Models.GenreModels.GenreDtos;

namespace Cinema.API.Models.MovieModels.MovieDtos
{
    public class MovieDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

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
