using AutoMapper;
using Cinema.API.Models.CastModels.CastModelDtos;
using Cinema.API.Repositories.IRepositories;
using Cinema.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DirectorDto> GetDirectorByIdAsync(Guid id)
        {
            var director = await _unitOfWork.Directors.GetOrDefaultAsync(filter: d => d.Id == id, includeProperty: "Person");
            if (director is null)
                throw new ArgumentNullException(nameof(id), "Режиссер с таким ID не найден.");
            return _mapper.Map<DirectorDto>(director);
        }

        public async Task<IEnumerable<DirectorDto>> GetDirectorsAsync()
        {
            var directors = await _unitOfWork.Directors.GetAllEntities(includeProperty: "Person")
                                                       .ToListAsync();
            return _mapper.Map<IEnumerable<DirectorDto>>(directors);
        }
    }
}
