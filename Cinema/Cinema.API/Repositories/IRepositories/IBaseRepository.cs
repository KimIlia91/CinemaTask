using System.Linq.Expressions;

namespace Cinema.API.Repositories.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получает все записи из таблицы типа TEntity из базы данных.
        /// </summary>
        /// <param name="tracked">Указывает, трекать ли изменения.</param>
        /// <param name="includeProperty">Указывает, какие свойства загрузить вместе с типом TEntity.</param>
        /// <param name="pageSize">Указывает размер страницы.</param>
        /// <param name="page">Указывает номер страницы.</param>
        /// <returns>Возвращает коллекцию всех записей типа TEntity из базы данных.</returns>
        IQueryable<TEntity> GetAllEntities(Expression<Func<TEntity, bool>>? search = null,
                                                      bool tracked = false,
                                                      string? includeProperty = null);

        /// <summary>
        /// Получает сущность из контекста данных, удовлетворяющую условиям фильтрации, или возвращает null, если сущность не найдена.
        /// </summary>
        /// <param name="filter">Выражение фильтрации.</param>
        /// <param name="tracked">True, если нужно отслеживать изменения сущности.</param>
        /// <param name="includeProperty">Строка, указывающая, какие свойства навигации должны быть включены в запрос.</param>
        /// <returns>Сущность из контекста данных, удовлетворяющую условиям фильтрации, или null, если сущность не найдена.</returns>
        Task<TEntity?> GetOrDefaultAsync(Expression<Func<TEntity, bool>>? filter = null,
                                         bool tracked = false,
                                         string? includeProperty = null);

        /// <summary>
        /// Добавляет новую сущность в контекст данных.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        Task AddAndSaveAsync(TEntity entity);

        /// <summary>
        /// Удаляет сущность из контекста данных.
        /// </summary>
        /// <param name="entity">Удаляемая сущность.</param>
        Task DeleteAndSaveAsync(TEntity entity);

        /// <summary>
        /// Обнавляет сущность из контекста данных.
        /// </summary>
        /// <param name="entity">Обновляемая сущность.</param>
        Task UpdateAndSaveAsync(TEntity entity);

        Task<int> GetTotalCountOfEntity(IQueryable<TEntity> query);

        Task<IEnumerable<TEntity>> GetEntitiesForPageAsync(IQueryable<TEntity> query, int page, int pageSize);

        Task SaveAsync();
    }
}
