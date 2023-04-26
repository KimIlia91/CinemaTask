using System.ComponentModel.DataAnnotations;

namespace Cinema.WEB.Models.PersonModels.PersonVms
{
    public class PersonVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; } = null!;
    }
}
