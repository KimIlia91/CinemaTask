using Cinema.API.Models.CastModels;

namespace Cinema.API.Repositories.IRepositories
{
    public interface IDirectorRepository : IBaseRepository<Director>
    {
        Task<List<Director>> GetDirectorsByIdsAsync(List<Guid> directorIds, string? includeProperty = null);
    }
}
