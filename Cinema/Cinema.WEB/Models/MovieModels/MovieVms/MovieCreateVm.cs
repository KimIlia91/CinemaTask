using Cinema.WEB.Helpers;
using Cinema.WEB.Models.MovieModels.MovieDtos;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Cinema.WEB.Models.MovieModels.MovieVms
{
    public class MovieCreateVm
    {
        public MovieCreateDto Movie { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg" }, ErrorMessage = "Разрешены только файлы JPEG")]
        [FileSize(50 * 1024 * 1024, ErrorMessage = "Размер файла не должен превышать 50 МБ")]
        public IFormFile? ImageFile { get; set; }

        [AllowedExtensions(new string[] { ".mp4", ".avi", ".mov", ".mkv", ".AVI", ".MPEG", ".MOV", ".WMV", ".MP4", ".FLV", ".WebM" })]
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
