using AutoMapper;
using Cinema.API.Models.PersonModels.PersonDtos;
using Cinema.API.Models.PersonModels;

namespace Cinema.API.Data.MappingConfigurations
{
    public class PersonToPersonDtoConverter : ITypeConverter<Person, PersonDto>
    {
        public PersonDto Convert(Person source, PersonDto destination, ResolutionContext context)
        {
            destination = new PersonDto
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                DateOfBirth = source.DateOfBirth,
                Country = source.Country,
                Age = source.Age,
                ImageUrl = source.ImageUrl,
                IsActor = source.Actor != null,
                IsDirector = source.Director != null,
                IsScreenwriter = source.Screenwriter != null,
            };

            return destination;
        }
    }
}
