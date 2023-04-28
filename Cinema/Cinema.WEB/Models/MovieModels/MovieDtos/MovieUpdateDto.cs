using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cinema.WEB.Models.MovieModels.MovieDtos
{
    public class MovieUpdateDto
    {
        public Guid Id { get; set; }

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
        [Display(Name = "Год выпуска")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Длительность фильма обязательно для заполнения")]
        [Display(Name = "Длительность в минутах")]
        public int RuntimeInMinutes { get; set; }

        [Required(ErrorMessage = "Страна обязательное поле")]
        [StringLength(50, ErrorMessage = "Длина поля должна быть не более 50 символов")]
        [Display(Name = "Страна")]
        public string Country { get; set; } = null!;

        [RegularExpression(@"^.*\.(jpg|JPG)$", ErrorMessage = "Только JPG файлы поддерживаются")]
        [Display(Name = "Изображение")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Видео")]
        public string? VideoUrl { get; set; }

        [Required(ErrorMessage = "Жанр обязательное поле.")]
        public Guid GenreId { get; set; }

        [Display(Name = "Актёры")]
        public List<Guid> Actors { get; set; } = new List<Guid>();

        [Display(Name = "Режиссеры")]
        public List<Guid> Directors { get; set; } = new List<Guid>();

        [Display(Name = "Сценаристы")]
        public List<Guid> Screenwriters { get; set; } = new List<Guid>();
    }
}
