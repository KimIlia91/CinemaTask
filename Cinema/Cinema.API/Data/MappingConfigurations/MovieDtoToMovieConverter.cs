using AutoMapper;
using Cinema.API.Models.CastModels;
using Cinema.API.Models.MovieModels.MovieDtos;
using Cinema.API.Models.MovieModels;

namespace Cinema.API.Data.MappingConfigurations
{
    public class MovieDtoToMovieConverter : ITypeConverter<MovieDto, Movie>
    {
        public Movie Convert(MovieDto source, Movie destination, ResolutionContext context)
        {
            destination ??= new Movie();

            destination.Id = source.Id;
            destination.Title = source.Title;
            destination.ShortDescription = source.ShortDescription;
            destination.Description = source.Description;
            destination.ReleaseYear = source.ReleaseYear;
            destination.RuntimeInMinutes = source.RuntimeInMinutes;
            destination.ImageUrl = source.ImageUrl;
            destination.VideoUrl = source.VideoUrl;
            destination.GenreId = source.Genre.Id;
            destination.Country = source.Country;
            destination.Directors = context.Mapper.Map<ICollection<Director>>(source.Directors);
            destination.Screenwriters = context.Mapper.Map<ICollection<Screenwriter>>(source.Screenwriters);
            destination.Actors = context.Mapper.Map<ICollection<Actor>>(source.Actors);

            return destination;
        }
    {
    }
}
