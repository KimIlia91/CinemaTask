using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Models.MovieModels.MovieDtos
{
    public class MovieCreateDto
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина названия фильма 1 - 50 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Полное описание обязательно для заполнения")]
        [StringLength(int.MaxValue, MinimumLength = 100, ErrorMessage = "Длина описания фильма не меньше 100 символов")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Год релиза обязателен для заполнения")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Год должен быть в формате yyyy")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Длительность фильма обязательно для заполнения")]
        public int RuntimeInMinutes { get; set; }

        [Required(ErrorMessage = "Страна обязательное поле")]
        [StringLength(50, ErrorMessage = "Длина поля должна быть не более 50 символов")]
        public string Country { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        [Required(ErrorMessage = "Жанр обязательное поле.")]
        public Guid GenreId { get; set; }

        public List<Guid> Actors { get; set; } = new List<Guid>();

        public List<Guid> Directors { get; set; } = new List<Guid>();

        public List<Guid> Screenwriters { get; set; } = new List<Guid>();
    }
}
