using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cinema.WEB.Models.MovieModels.MovieDtos
{
    public class MovieCreateDto
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина названия фильма 1 - 50 символов")]
        [Display(Name = "Наименование")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Полное описание обязательно для заполнения")]
        [StringLength(int.MaxValue, MinimumLength = 100, ErrorMessage = "Длина описания фильма не меньше 100 символов")]
        [Display(Name = "Полное описание")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Год релиза обязателен для заполнения")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Год должен быть в формате yyyy")]
        [Display(Name = "Год релиза")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Длительность фильма обязательно для заполнения")]
        [Display(Name = "Длительность фильма")]
        [Range(minimum: 1, maximum: int.MaxValue, ConvertValueInInvariantCulture = true, ErrorMessage = "Длительность фильма должна быть не меньше 1 минуту")]
        public int RuntimeInMinutes { get; set; }

        [Required(ErrorMessage = "Страна обязательное поле")]
        [StringLength(50, ErrorMessage = "Длина поля не более 50 символов")]
        [Display(Name = "Страна")]
        public string Country { get; set; } = null!;

        [Display(Name = "Изображение")]
        public string? ImageUrl { get; set; } = null!;

        [Display(Name = "Видео")]
        public string? VideoUrl { get; set; }

        [Required(ErrorMessage = "Жанр обязательное поле.")]
        [Display(Name = "Жанр")]
        public Guid GenreId { get; set; }

        [Display(Name = "Актёры")]
        public List<Guid> Actors { get; set; } = new List<Guid>();

        [Display(Name = "Режиссёры")]
        public List<Guid> Directors { get; set; } = new List<Guid>();

        [Display(Name = "Сценаристы")]
        public List<Guid> Screenwriters { get; set; } = new List<Guid>();
    }
}
