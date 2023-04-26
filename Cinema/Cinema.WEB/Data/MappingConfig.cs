using AutoMapper;
using Cinema.WEB.Models.GenreModels.GenreDtos;
using Cinema.WEB.Models.GenreModels.GenreVms;
using Cinema.WEB.Models.MovieModels.MovieDtos;
using Cinema.WEB.Models.PersonModels.PersonDtos;
using Cinema.WEB.Models.PersonModels.PersonVms;

namespace Cinema.WEB.Data
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<GenreVm, GenreDto>().ReverseMap();

            CreateMap<GenreCreateVM, GenreCreateDto>().ReverseMap();

            CreateMap<PersonDto, PersonVm>().ReverseMap();

            CreateMap<MovieDto, MovieUpdateDto>()
                .ConvertUsing<MovieDtoToMovieUpdateDtoConverter>();
        }
    }
}
