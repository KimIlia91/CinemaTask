using AutoMapper;
using Cinema.API.Models.MovieModels.MovieDtos;
using Cinema.API.Models.MovieModels;
using Cinema.API.Models.GenreModels.GenreDtos;
using Cinema.API.Models.CastModels.CastModelDtos;

namespace Cinema.API.Data.MappingConfigurations
{
    public class MovieToMovieDtoConverter : ITypeConverter<Movie, MovieDto>
    {
        public MovieDto Convert(Movie source, MovieDto destination, ResolutionContext context)
        {
            destination ??= new MovieDto();

            destination.Id = source.Id;
            destination.Title = source.Title;
            destination.VideoUrl = source.VideoUrl;
            destination.ImageUrl = source.ImageUrl;
            destination.Description = source.Description;
            destination.ShortDescription = source.ShortDescription;
            destination.ReleaseYear = source.ReleaseYear;
            destination.RuntimeInMinutes = source.RuntimeInMinutes;
            destination.Country = source.Country;
            destination.Genre = context.Mapper.Map<GenreDto>(source.Genre);
            destination.Actors = context.Mapper.Map<ICollection<ActorDto>>(source.Actors);
            destination.Directors = context.Mapper.Map<ICollection<DirectorDto>>(source.Directors);
            destination.Screenwriters = context.Mapper.Map<ICollection<ScreenwriterDto>>(source.Screenwriters);

            return destination;
        }
    }
}
