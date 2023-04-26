using AutoMapper;
using Cinema.WEB.Helpers;
using Cinema.WEB.Models.GenreModels.GenreDtos;
using Cinema.WEB.Models.GenreModels.GenreVms;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WEB.Controllers
{
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: GenreController
        public async Task<IActionResult> GenreIndex()
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var genreDtos = await _unitOfWork.Genres.GetAllGenersAsync(token!);
            var genreVms = _mapper.Map<List<GenreVm>>(genreDtos);

            if (genreDtos.Any())
            {
                return View(genreVms);
            }

            return View(genreVms);
        }

        // GET: GenreController/Create
        public ActionResult GenreCreate()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenreCreate(GenreCreateVM genreVm)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString(SD.SessionToken);
                var genreDto = _mapper.Map<GenreCreateDto>(genreVm);
                var isSuccess = await _unitOfWork.Genres.CreateGenreAsync(genreDto, token!);
                if (isSuccess)
                {
                    TempData["success"] = "Жанр успешно добавлен!";
                    return RedirectToAction(nameof(GenreIndex));
                }
            }

            TempData["error"] = "Oops! Неудалось добавить жанр.";
            return View(genreVm);
        }

        // GET: GenreController/Edit
        public async Task<IActionResult> GenreUpdate(Guid id)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var genreDto = await _unitOfWork.Genres.GetGenreAsync(id, token!);

            if (genreDto != null)
            {
                var genreVm = _mapper.Map<GenreVm>(genreDto);
                return View(genreVm);
            }

            return RedirectToAction(nameof(GenreIndex));
        }

        // POST: GenreController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenreUpdate(GenreVm genreVm)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString(SD.SessionToken);
                var genreDto = _mapper.Map<GenreDto>(genreVm);
                var isSuccess = await _unitOfWork.Genres.UpdateGenreAsync(genreDto, token!);
                if (isSuccess)
                {
                    TempData["success"] = "Жанр успешно обновлён!";
                    return RedirectToAction(nameof(GenreIndex));
                }
            }

            TempData["error"] = "Oops! Неудалось обновить жанр.";
            return View(genreVm);
        }

        // POST: GenreController/GenreDelete/5
        public async Task<IActionResult> GenreDelete(Guid id)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var genreToDelete = await _unitOfWork.Genres.GetGenreAsync(id, token!);

            if (genreToDelete is not null)
            {
                var isSuccess = await _unitOfWork.Genres.DeleteGenreAsync(genreToDelete.Id, token!);
                if (isSuccess)
                {
                    TempData["success"] = "Жанр успешно удален!";
                    return RedirectToAction(nameof(GenreIndex));
                }
            }

            TempData["error"] = "Oops! Неудалось удалить жанр.";
            return RedirectToAction(nameof(GenreIndex));
        }
    }
}
