using AutoMapper;
using Cinema.WEB.Helpers;
using Cinema.WEB.Models.MovieModels.MovieVms;
using Cinema.WEB.Models.MovieModels;
using Microsoft.AspNetCore.Mvc;
using Cinema.WEB.Services.IServices;
using Cinema.WEB.Models.MovieModels.MovieDtos;

namespace Cinema.WEB.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //GET: MovieController/MovieIndex
        public async Task<IActionResult> MovieIndex(
            bool sort,
            string? titleFilter = null,
            string? genreFilter = null,
            string? directorFilter = null,
            string? search = null,
            int page = 1, int pageSize = 20)
        {
            var request = new MoviesFilterRequest(search, titleFilter, directorFilter, genreFilter, sort, pageSize, page);
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var pageResponse = await _unitOfWork.Movies.GetAllMoviesAsync(token!, request);
            if (pageResponse is not null)
            {
                return View(pageResponse);
            }

            return View(new MovieFilteredResponse());
        }

        //GET: MovieController/MovieDetails/5
        public async Task<IActionResult> MovieDetails(Guid id)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var movieDto = await _unitOfWork.Movies.GetMovieAsync(id, token!);
            if (movieDto is null)
            {
                TempData["error"] = "Oops! Неудалось найти фильм.";
                return RedirectToAction(nameof(MovieIndex));
            }

            var movieVm = _mapper.Map<MovieVm>(movieDto);   
            return View(movieVm);
        }

        //GET: MovieController/MovieCreate
        public async Task<IActionResult> MovieCreate()
        {
            var movieVm = new MovieCreateVm();
            var token = HttpContext.Session.GetString(SD.SessionToken);
            movieVm.ActorList = await _unitOfWork.Actors.GetSelectListOfActorsAsync(token!, movieVm.Movie.Actors);
            movieVm.DirectorList = await _unitOfWork.Directors.GetSelectListOfDirectorsAsync(token!, movieVm.Movie.Directors);
            movieVm.ScreenwriterList = await _unitOfWork.Screenwriters.GetSelectListOfScreenwritersAsync(
                token!, movieVm.Movie.Screenwriters);
            movieVm.GenreList = await _unitOfWork.Genres.GetSelectListOfGenresAsync(token!);
            return View(movieVm);
        }

        // POST: MovieController/MovieCreate
        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> MovieCreate(MovieCreateVm movieVm)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            if (ModelState.IsValid)
            {
                movieVm.Movie.ImageUrl = await _unitOfWork.Images.SaveImageAsync(movieVm.ImageFile, movieVm.Movie.ImageUrl, @"images\movies");
                movieVm.Movie.VideoUrl = await _unitOfWork.Videos.SaveVideoAsync(movieVm.VideoFile, movieVm.Movie.VideoUrl, @"videos\movies");
                var response = await _unitOfWork.Movies.CreateMovieAsync(movieVm.Movie, token!);
                if (response.IsSuccess)
                {
                    TempData["success"] = "Информация успешно добавлена!";
                    return RedirectToAction(nameof(MovieIndex));
                }

                _unitOfWork.Images.DeleteImage(movieVm.Movie.ImageUrl);
                _unitOfWork.Videos.DeleteVideo(movieVm.Movie.VideoUrl);
                movieVm.Movie.ImageUrl = null;
                movieVm.Movie.VideoUrl = null;
                movieVm.ActorList = await _unitOfWork.Actors.GetSelectListOfActorsAsync(token!, movieVm.Movie.Actors);
                movieVm.DirectorList = await _unitOfWork.Directors.GetSelectListOfDirectorsAsync(token!, movieVm.Movie.Directors);
                movieVm.ScreenwriterList = await _unitOfWork.Screenwriters.GetSelectListOfScreenwritersAsync(
                    token!, movieVm.Movie.Screenwriters);
                movieVm.GenreList = await _unitOfWork.Genres.GetSelectListOfGenresAsync(token!);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["error"] = string.Join("\n", response.ErrorMessage);
                    return View(movieVm);
                }
            }

            TempData["error"] = "Oops! Не удалось добавить информацию.";
            return View(movieVm);
        }

        //GET: MovieController/MovieUpdate/1
        public async Task<IActionResult> MovieUpdate(Guid id)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var movieDto = await _unitOfWork.Movies.GetMovieAsync(id, token!);
            var movieVm = new MovieUpdateVm
            {
                Movie = _mapper.Map<MovieUpdateDto>(movieDto)
            };

            if (movieVm.Movie is null)
            {
                return RedirectToAction(nameof(MovieIndex));
            }

            movieVm.ActorList = await _unitOfWork.Actors.GetSelectListOfActorsAsync(token!, movieVm.Movie.Actors);
            movieVm.DirectorList = await _unitOfWork.Directors.GetSelectListOfDirectorsAsync(token!, movieVm.Movie.Directors);
            movieVm.ScreenwriterList = await _unitOfWork.Screenwriters.GetSelectListOfScreenwritersAsync(
                token!, movieVm.Movie.Screenwriters);
            movieVm.GenreList = await _unitOfWork.Genres.GetSelectListOfGenresAsync(token!);
            return View(movieVm);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieUpdate(MovieUpdateVm movieVm)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            if (ModelState.IsValid)
            {
                var oldImgPath = movieVm.Movie.ImageUrl;
                var oldVideoPath = movieVm.Movie.VideoUrl;
                movieVm.Movie.ImageUrl = await _unitOfWork.Images.SaveImageAsync(movieVm.ImageFile, movieVm.Movie.ImageUrl, @"images\movies");
                movieVm.Movie.VideoUrl = await _unitOfWork.Videos.SaveVideoAsync(movieVm.VideoFile, movieVm.Movie.VideoUrl, @"videos\movies");
                var response = await _unitOfWork.Movies.UpdateMovieAsync(movieVm.Movie, token!);
                if (response.IsSuccess)
                {
                    TempData["success"] = "Информация успешно обновлена!";
                    _unitOfWork.Images.DeleteImage(oldImgPath);
                    _unitOfWork.Videos.DeleteVideo(oldVideoPath);
                    return RedirectToAction(nameof(MovieIndex));
                }

                _unitOfWork.Images.DeleteImage(movieVm.Movie.ImageUrl);
                _unitOfWork.Videos.DeleteVideo(movieVm.Movie.VideoUrl);
                movieVm.Movie.ImageUrl = oldImgPath;
                movieVm.Movie.VideoUrl = oldVideoPath;
                movieVm.ActorList = await _unitOfWork.Actors.GetSelectListOfActorsAsync(token!, movieVm.Movie.Actors);
                movieVm.DirectorList = await _unitOfWork.Directors.GetSelectListOfDirectorsAsync(token!, movieVm.Movie.Directors);
                movieVm.ScreenwriterList = await _unitOfWork.Screenwriters.GetSelectListOfScreenwritersAsync(token!, movieVm.Movie.Screenwriters);
                movieVm.GenreList = await _unitOfWork.Genres.GetSelectListOfGenresAsync(token!);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["error"] = string.Join("\n", response.ErrorMessage);
                    return View(movieVm);
                }
            }

            TempData["error"] = "Oops! Не удалось обновить информацию.";
            return View(movieVm);
        }

        //POST: MovieController/MovieDelete
        public async Task<IActionResult> MovieDelete(Guid id)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var movieToDelete = await _unitOfWork.Movies.GetMovieAsync(id, token!);
            if (movieToDelete is not null)
            {
                var isSuccess = await _unitOfWork.Movies.DeleteMovieAsync(id, token!);
                if (isSuccess)
                {
                    if (movieToDelete.ImageUrl is not null)
                        _unitOfWork.Images.DeleteImage(movieToDelete.ImageUrl);
                    if (movieToDelete.VideoUrl is not null)
                        _unitOfWork.Videos.DeleteVideo(movieToDelete?.VideoUrl);
                    TempData["success"] = "Информация успешно удалена!";
                    return RedirectToAction(nameof(MovieIndex));
                }
            }

            TempData["error"] = "Oops! Не удалось удалить информация.";
            return RedirectToAction(nameof(MovieIndex));
        }
    }
}
