using AutoMapper;
using Cinema.API.Models.CastModels.CastModelDtos;
using Cinema.API.Repositories.IRepositories;
using Cinema.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Services
{
    public class ScreenwriterService : IScreenwriterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScreenwriterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ScreenwriterDto> GetScreenwriterByIdAsync(Guid id)
        {
            var screenwriter = await _unitOfWork.Screenwriters.GetOrDefaultAsync(filter: s => s.Id == id, includeProperty: "Person", tracked: false);
            if (screenwriter is null)
                throw new ArgumentNullException(nameof(id), "Сценарист с таким ID не найден.");
            return _mapper.Map<ScreenwriterDto>(screenwriter);
        }

        public async Task<IEnumerable<ScreenwriterDto>> GetScreenwritersAsync()
        {
            var screenwriters = await _unitOfWork.Screenwriters.GetAllEntities(includeProperty: "Person", tracked: false)
                                                               .ToListAsync();
            return _mapper.Map<IEnumerable<ScreenwriterDto>>(screenwriters);
        }
    }
}
