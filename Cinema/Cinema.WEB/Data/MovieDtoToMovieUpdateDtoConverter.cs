using AutoMapper;
using Cinema.WEB.Models.MovieModels.MovieDtos;

namespace Cinema.WEB.Data
{
    public class MovieDtoToMovieUpdateDtoConverter : ITypeConverter<MovieDto, MovieUpdateDto>
    {
        public MovieUpdateDto Convert(MovieDto source, MovieUpdateDto destination, ResolutionContext context)
        {
            destination ??= new MovieUpdateDto();

            destination.Id = source.Id;
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.ShortDescription = source.ShortDescription;
            destination.Country = source.Country;
            destination.ReleaseYear = source.ReleaseYear;
            destination.RuntimeInMinutes = source.RuntimeInMinutes;
            destination.VideoUrl = source.VideoUrl;
            destination.ImageUrl = source.ImageUrl;
            destination.GenreId = source.Genre.Id;
            destination.Actors = source.Actors.Select(x => x.Id).ToList();
            destination.Directors = source.Directors.Select(x => x.Id).ToList();
            destination.Screenwriters = source.Screenwriters.Select(x => x.Id).ToList();

            return destination;
        }
    }
}
