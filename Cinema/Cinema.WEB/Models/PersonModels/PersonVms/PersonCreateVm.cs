using Cinema.WEB.Models.PersonModels.PersonDtos;

namespace Cinema.WEB.Models.PersonModels.PersonVms
{
    public class PersonCreateVm
    {
        public PersonCreateDto Person { get; set; }

        public IFormFile? ImageFile { get; set; }

        public PersonCreateVm()
        {
            Person = new PersonCreateDto();
        }
    }
}
