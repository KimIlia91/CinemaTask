using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Models.GenreModels.GenreDtos
{
    public class GenreDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование обязательное поле.")]
        [StringLength(50, ErrorMessage = "Наименое должно быть не длинее 50 символов.")]
        public string Name { get; set; } = null!;
    }
}
