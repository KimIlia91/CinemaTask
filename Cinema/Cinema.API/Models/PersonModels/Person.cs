using Cinema.API.Models.CastModels;
using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Models.PersonModels
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина названия фильма 1 - 50 символов")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(50, ErrorMessage = "Длина названия фильма 1 - 50 символов")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Дата рождения обязательна для заполнения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Range(1, 200, ErrorMessage = "Возраст должен быть от 1 до 200")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Страна обязательна для заполнения")]
        [StringLength(60, ErrorMessage = "Длина названия фильма 1 - 60 символов")]
        public string Country { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public Actor? Actor { get; set; }

        public Director? Director { get; set; }

        public Screenwriter? Screenwriter { get; set; }
    }
}
