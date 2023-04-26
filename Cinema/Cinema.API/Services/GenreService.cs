using AutoMapper;
using Cinema.API.Models.GenreModels.GenreDtos;
using Cinema.API.Models.GenreModels;
using Cinema.API.Repositories.IRepositories;
using Cinema.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _unitOfWork.Genres.GetAllEntities()
                                                 .ToListAsync();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task CreateGenreAsync(GenreCreateDto genreCreateDto)
        {
            var genre = await _unitOfWork.Genres.GetOrDefaultAsync(g => g.Name.ToLower() == genreCreateDto.Name.ToLower());
            if (genre != null)
                throw new ArgumentException("Такой жанр уже существует.");

            genre = _mapper.Map<Genre>(genreCreateDto);
            await _unitOfWork.Genres.AddAndSaveAsync(genre);
        }

        public async Task<GenreDto> GetGenreByIdAsync(Guid id)
        {
            var genre = await _unitOfWork.Genres.GetOrDefaultAsync(g => g.Id == id);
            if (genre is null)
                throw new ArgumentNullException(nameof(id), "Жанра с таким идентификационным номером не найден.");

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task UpdateGenreAsync(GenreDto genreDto)
        {
            var isGenreExist = await _unitOfWork.Genres.GetOrDefaultAsync(g => g.Name.ToLower() == genreDto.Name.ToLower());
            if (isGenreExist != null)
                throw new ArgumentException("Такой жанр уже существует.");

            var genre = await _unitOfWork.Genres.GetOrDefaultAsync(g => g.Id == genreDto.Id);
            if (genre is null)
                throw new ArgumentNullException(nameof(genreDto), "Жанр для обновления не найден.");

            genre.Name = genreDto.Name;
            await _unitOfWork.Genres.UpdateAndSaveAsync(genre);
        }

        public async Task DeleteGenreByIdAsync(Guid id)
        {
            var genreToDelete = await _unitOfWork.Genres.GetOrDefaultAsync(g => g.Id == id);
            if (genreToDelete is null)
                throw new ArgumentNullException(nameof(id), "Жанр с таким идентификацонным номер не найден.");

            await _unitOfWork.Genres.DeleteAndSaveAsync(genreToDelete);
        }
    }
}
