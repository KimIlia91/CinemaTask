using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cinema.WEB.Models.PersonModels.PersonDtos
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина названия фильма 1 - 50 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(50, ErrorMessage = "Длина названия фильма 1 - 50 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Дата рождения обязательна для заполнения")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Range(1, 200, ErrorMessage = "Возраст должен быть от 1 до 200")]
        [Required(ErrorMessage = "Возраст обязателен для заполнения")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Страна обязательна для заполнения")]
        [StringLength(60, ErrorMessage = "Длина названия фильма 1 - 60 символов")]
        [Display(Name = "Страна")]
        public string Country { get; set; } = null!;

        [Display(Name = "URL фото")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Актёр")]
        public bool IsActor { get; set; } = false;

        [Display(Name = "Режиссёр")]
        public bool IsDirector { get; set; } = false;

        [Display(Name = "Сценарист")]
        public bool IsScreenwriter { get; set; } = false;
    }
}
