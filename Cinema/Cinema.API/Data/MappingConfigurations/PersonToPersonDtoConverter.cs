using AutoMapper;
using Cinema.API.Models.PersonModels.PersonDtos;
using Cinema.API.Models.PersonModels;

namespace Cinema.API.Data.MappingConfigurations
{
    public class PersonToPersonDtoConverter : ITypeConverter<Person, PersonDto>
    {
        public PersonDto Convert(Person source, PersonDto destination, ResolutionContext context)
        {
            destination ??= new PersonDto();

            destination.Id = source.Id;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.DateOfBirth = source.DateOfBirth;
            destination.Country = source.Country;
            destination.Age = source.Age;
            destination.ImageUrl = source.ImageUrl;
            destination.IsActor = source.Actor != null;
            destination.IsDirector = source.Director != null;
            destination.IsScreenwriter = source.Screenwriter != null;

            return destination;
        }
    }
}
