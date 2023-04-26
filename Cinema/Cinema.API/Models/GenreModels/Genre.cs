using Cinema.API.Models.MovieModels;
using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Models.GenreModels
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование обязательное поле.")]
        [StringLength(50, ErrorMessage = "Наименое должно быть не длинее 50 символов.")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
