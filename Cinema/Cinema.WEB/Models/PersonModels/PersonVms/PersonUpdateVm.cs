using Cinema.WEB.Models.PersonModels.PersonDtos;

namespace Cinema.WEB.Models.PersonModels.PersonVms
{
    public class PersonUpdateVm
    {
        public PersonDto Person { get; set; }

        public IFormFile? ImageFile { get; set; }

        public PersonUpdateVm()
        {
            Person = new PersonDto();
        }
    }
}
