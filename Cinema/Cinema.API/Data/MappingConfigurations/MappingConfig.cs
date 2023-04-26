using AutoMapper;
using Cinema.API.Models.CastModels.CastModelDtos;
using Cinema.API.Models.CastModels;
using Cinema.API.Models.GenreModels.GenreDtos;
using Cinema.API.Models.GenreModels;
using Cinema.API.Models.MovieModels.MovieDtos;
using Cinema.API.Models.MovieModels;
using Cinema.API.Models.PersonModels.PersonDtos;
using Cinema.API.Models.PersonModels;
using System.ComponentModel;

namespace Cinema.API.Data.MappingConfigurations
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            #region Person/PersonCreateDto

            CreateMap<PersonCreateDto, Person>()
                .ForMember(dest => dest.Actor, opt => opt.MapFrom(
                    src => src.IsActor ? new Actor() : null))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(
                    src => src.IsDirector ? new Director() : null))
                .ForMember(dest => dest.Screenwriter, opt => opt.MapFrom(
                    src => src.IsScreenwriter ? new Screenwriter() : null))
                .ReverseMap();

            #endregion

            #region Person/PersonDto

            CreateMap<PersonDto, Person>()
                .ConvertUsing<PersonDtoToPersonConverter>();

            CreateMap<Person, PersonDto>()
                .ConvertUsing<PersonToPersonDtoConverter>();

            #endregion

            #region Genre/GenreDto

            CreateMap<Genre, GenreDto>().ReverseMap();

            #endregion

            #region Genre/GenreCreateDto

            CreateMap<Genre, GenreCreateDto>().ReverseMap();

            #endregion

            #region Movie/MovieDto

            CreateMap<Movie, MovieDto>()
                .ConvertUsing<MovieToMovieDtoConverter>();

            CreateMap<MovieDto, Movie>()
                .ConvertUsing<MovieDtoToMovieConverter>();

            #endregion

            #region MoviesFilterRequest/MoviesFilteredResponse

            CreateMap<MoviesFilterRequest, MoviesFilteredResponse>()
                .ForMember(dest => dest.MoviesPage, opt => opt.MapFrom(src =>
                    new Page<MovieDto>(new List<MovieDto>(), src.Page, src.PageSize, 0)));

            #endregion

            #region PersonsFilterRequest/PersonsFilteredResponse

            CreateMap<PersonsFilterRequest, PersonsFilteredResponse>()
                .ForMember(dest => dest.PersonsPage, opt => opt.MapFrom(src =>
                    new Page<PersonDto>(new List<PersonDto>(), src.Page, src.PageSize, 0)));

            #endregion

            #region MovieCreateDto/Movie

            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.Directors, opt => opt.Ignore())
                .ForMember(dest => dest.Actors, opt => opt.Ignore())
                .ForMember(dest => dest.Screenwriters, opt => opt.Ignore());

            #endregion

            #region MovieUpdateDto/Movie

            CreateMap<MovieUpdateDto, Movie>()
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors.Select(id => new Actor { Id = id }).ToList()))
                .ForMember(dest => dest.Directors, opt => opt.MapFrom(src => src.Directors.Select(id => new Director { Id = id }).ToList()))
                .ForMember(dest => dest.Screenwriters, opt => opt.MapFrom(src => src.Screenwriters.Select(id => new Screenwriter { Id = id }).ToList()))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

            #endregion

            #region Actor/ActorDto

            CreateMap<Actor, ActorDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ActorDto, Actor>()
                .ForMember(dest => dest.Movies, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore());

            CreateMap<Guid, Actor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(actorId => actorId));

            #endregion

            #region Director/DirectorDto

            CreateMap<Director, DirectorDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<DirectorDto, Director>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore());

            CreateMap<Guid, Director>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(directorId => directorId));

            #endregion

            #region Screenwriter/ScreenwriterDto

            CreateMap<Screenwriter, ScreenwriterDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ScreenwriterDto, Screenwriter>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore());

            CreateMap<Guid, Screenwriter>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(screenwriterId => screenwriterId));

            #endregion
        }
    }
}
