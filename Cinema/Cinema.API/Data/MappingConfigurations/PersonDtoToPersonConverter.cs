using AutoMapper;
using Cinema.API.Models.CastModels;
using Cinema.API.Models.PersonModels.PersonDtos;
using Cinema.API.Models.PersonModels;

namespace Cinema.API.Data.MappingConfigurations
{
    public class PersonDtoToPersonConverter : ITypeConverter<PersonDto, Person>
    {
        public Person Convert(PersonDto source, Person destination, ResolutionContext context)
        {
            destination ??= new Person();

            destination.Id = source.Id;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.DateOfBirth = source.DateOfBirth;
            destination.Age = source.Age;
            destination.Country = source.Country;
            destination.ImageUrl = source.ImageUrl;
            destination.Actor = source.IsActor ? new Actor() : null;
            destination.Director = source.IsDirector ? new Director() : null;
            destination.Screenwriter = source.IsScreenwriter ? new Screenwriter() : null;

            return destination;
        }
    }
}
