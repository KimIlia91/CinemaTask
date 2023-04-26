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
            destination = new Person
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                DateOfBirth = source.DateOfBirth,
                Age = source.Age,
                Country = source.Country,
                ImageUrl = source.ImageUrl,
                Actor = source.IsActor ? new Actor() : null,
                Director = source.IsDirector ? new Director() : null,
                Screenwriter = source.IsScreenwriter ? new Screenwriter() : null
            };

            return destination;
        }
    }
}
