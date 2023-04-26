using Cinema.API.Data;
using Cinema.API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Cinema.API.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseRepository{T}"/>.
        /// </summary>
        /// <param name="db">Контекст базы данных <see cref="ApplicationDbContext"/>.</param>
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        /// <inheritdoc/>
        public IQueryable<T> GetAllEntities(Expression<Func<T, bool>>? search = null,
                                                      bool tracked = false,
                                                      string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
                query = query.AsNoTracking();

            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (search != null)
                query = query.Where(search);

            return query;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetEntitiesForPageAsync(IQueryable<T> query, int page, int pageSize)
        {
            if (pageSize > 0)
            {
                if (pageSize > 100)
                    pageSize = 100;

                query = query.Skip(pageSize * (page - 1)).Take(pageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<int> GetTotalCountOfEntity(IQueryable<T> query) => await query.CountAsync();

        /// <inheritdoc/>
        public async Task<T?> GetOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null, bool tracked = false, string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task AddAndSaveAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAndSaveAsync(T entity)
        {
            if (entity != null)
            {
                dbSet.Update(entity);
                await SaveAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(entity), "Параметр не может быть NULL.");
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAndSaveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        /// <inheritdoc/>
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
