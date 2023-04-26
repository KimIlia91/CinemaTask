using AutoMapper;
using Cinema.API.Models.CastModels.CastModelDtos;
using Cinema.API.Repositories.IRepositories;
using Cinema.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActorDto> GetActorByIdAsync(Guid id)
        {
            var actors = await _unitOfWork.Actors.GetOrDefaultAsync(filter: a => a.Id == id, includeProperty: "Person");
            if (actors is null)
                throw new ArgumentNullException(nameof(id), "Актёр не найден.");
            return _mapper.Map<ActorDto>(actors);
        }

        public async Task<IEnumerable<ActorDto>> GetAllActorsAsync()
        {
            var actors = await _unitOfWork.Actors.GetAllEntities(includeProperty: "Person")
                                                 .ToListAsync();
            return _mapper.Map<IEnumerable<ActorDto>>(actors);
        }
    }
}
