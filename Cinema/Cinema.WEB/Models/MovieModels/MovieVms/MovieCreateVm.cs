using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.WEB.Models.MovieModels.MovieVms
{
    public class MovieCreateVm
    {
        public MovieCreateDto Movie { get; set; }

        public IFormFile? ImageFile { get; set; }

        public IFormFile? VideoFile { get; set; }

        public IEnumerable<SelectListItem> GenreList { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> DirectorList { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ScreenwriterList { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ActorList { get; set; } = new List<SelectListItem>();

        public MovieCreateVm()
        {
            Movie = new MovieCreateDto();
        }
    }
}
