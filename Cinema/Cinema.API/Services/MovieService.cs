using AutoMapper;
using Cinema.API.Models.MovieModels.MovieDtos;
using Cinema.API.Models.MovieModels;
using Cinema.API.Repositories.IRepositories;
using Cinema.API.Services.IServices;
using Cinema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Page<MovieDto>> GetFilteredPageOfMoviesAsync(MoviesFilterRequest opt)
        {
            var moviesQuery = _unitOfWork.Movies.GetFilteredMoviesQuery(
                opt.Search, opt.TitleFilter, opt.DirectorFilter, opt.GenreFilter);
            moviesQuery = _unitOfWork.Movies.GetSortedMoviesQuery(moviesQuery, opt.Sort);
            var totalFilteredMovies = await _unitOfWork.Movies.GetTotalCountOfEntity(moviesQuery);
            var movies = await _unitOfWork.Movies.GetEntitiesForPageAsync(moviesQuery, opt.Page, opt.PageSize);
            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return new Page<MovieDto>(movieDtos, opt.Page, opt.PageSize, totalFilteredMovies);
        }

        public async Task CreateMovieAsync(MovieCreateDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            var titleExist = await _unitOfWork.Movies.GetAllEntities(m => m.Title == movieDto.Title)
                                                     .ToListAsync();
            if (titleExist.Count != 0)
                throw new ArgumentException("Фильм с таким именем уже существует.");

            movie.Actors = await _unitOfWork.Actors.GetActorsByIdsAsync(movieDto.Actors);
            movie.Directors = await _unitOfWork.Directors.GetDirectorsByIdsAsync(movieDto.Directors);
            movie.Screenwriters = await _unitOfWork.Screenwriters.GetScreenwritersByIdsAsync(movieDto.Screenwriters);
            await _unitOfWork.Movies.AddAndSaveAsync(movie);
        }

        public async Task<MovieDto> GetMovieByIdAsync(Guid id)
        {
            var movie = await _unitOfWork.Movies.GetMovieIncludePersonAsync(id);

            if (movie is null)
                throw new ArgumentNullException(nameof(id), "Фильм не найден.");

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task DeleteMovieByIdAsync(Guid id)
        {
            var movie = await _unitOfWork.Movies.GetOrDefaultAsync(filter: m => m.Id == id);

            if (movie is null)
                throw new KeyNotFoundException($"Фильм с таким ключом {id} не найден.");

            await _unitOfWork.Movies.DeleteAndSaveAsync(movie);
        }

        public async Task UpdateMovieAsync(MovieUpdateDto movieDto)
        {
            var movie = await _unitOfWork.Movies.GetOrDefaultAsync(m => m.Id == movieDto.Id,
                                                                   includeProperty: "Actors,Screenwriters,Genre,Directors");
            if (movie is null)
                throw new ArgumentNullException(nameof(movieDto), "Фильм не найден.");

            var moviesWithSameTitle = await _unitOfWork.Movies.GetAllEntities(m => m.Title == movieDto.Title &&
                                                                                   m.Id != movieDto.Id)
                                                              .ToListAsync();
            if (moviesWithSameTitle.Any())
                throw new ArgumentException("Фильм с таким названием уже существует.");

            movie = _mapper.Map(movieDto, movie);
            await _unitOfWork.Movies.UpdateMovieAndSaveAsync(movie);
        }
    }
}
