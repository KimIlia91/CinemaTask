using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cinema.WEB.Models.GenreModels.GenreVms
{
    public class GenreVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; } = string.Empty;
    }
}
