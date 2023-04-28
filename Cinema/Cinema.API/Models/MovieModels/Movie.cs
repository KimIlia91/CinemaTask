using Cinema.API.Models.CastModels;
using Cinema.API.Models.GenreModels;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Cinema.API.Models.MovieModels
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина названия фильма 1 - 50 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Полное описание обязательно для заполнения")]
        [StringLength(int.MaxValue, MinimumLength = 100, ErrorMessage = "Длина описания фильма не меньше 100 символов")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Год релиза обязателен для заполнения")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Год должен быть в формате yyyy")]
        public int ReleaseYear { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "Длительность фильма обязательно для заполнения")]
        public int RuntimeInMinutes { get; set; }

        [Required(ErrorMessage = "Страна обязтальное поле")]
        [StringLength(50, ErrorMessage = "Длина строки для страны должна быть не более 50 символов")]
        public string Country { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public Guid GenreId { get; set; }

        public Genre Genre { get; set; } = null!;

        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

        public virtual ICollection<Director> Directors { get; set; } = new List<Director>();

        public virtual ICollection<Screenwriter> Screenwriters { get; set; } = new List<Screenwriter>();
    }
}
