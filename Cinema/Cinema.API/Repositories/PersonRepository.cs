using Cinema.API.Data;
using Cinema.API.Models.PersonModels;
using Cinema.API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Person> GetPersonsQuery(string? search)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.Persons.Include(p => p.Screenwriter)
                              .Include(p => p.Director)
                              .Include(p => p.Actor)
                              .Where(m => (string.IsNullOrEmpty(search) ||
                                           m.LastName.Contains(search) ||
                                           m.FirstName.Contains(search)));
        }


        public IQueryable<Person> GetSortedPersonsQuery(IQueryable<Person> query, bool sort = true) =>
            sort ? query.OrderByDescending(m => m.LastName) : query.OrderBy(m => m.LastName);

        /// <inheritdoc/>
        public async Task UpdatePersonAndSaveAsync(Person newPerson)
        {
            var currentPerson = await GetPersonIncludeOrDefaultAsync(newPerson.Id);
            currentPerson!.Actor = UpdateRelatedEntity(newPerson.Actor, currentPerson!.Actor);
            currentPerson!.Director = UpdateRelatedEntity(newPerson.Director, currentPerson.Director);
            currentPerson!.Screenwriter = UpdateRelatedEntity(newPerson.Screenwriter, currentPerson.Screenwriter);
            currentPerson.FirstName = newPerson.FirstName;
            currentPerson.LastName = newPerson.LastName;
            currentPerson.Age = newPerson.Age;
            currentPerson.Country = newPerson.Country;
            currentPerson.DateOfBirth = newPerson.DateOfBirth;
            currentPerson.ImageUrl = newPerson.ImageUrl;
            _db.Persons.Update(currentPerson);
            await SaveAsync();
        }

        private async Task<Person?> GetPersonIncludeOrDefaultAsync(Guid id) =>
          await _db.Persons.Include(p => p.Actor)
                           .Include(p => p.Screenwriter)
                           .Include(p => p.Director)
                           .FirstOrDefaultAsync(p => p.Id == id);

        /// <summary>
        /// Обновляет связанную с персоной сущность (Actor, Screenwriter, Director).
        /// </summary>
        /// <typeparam name="T">Тип связанной сущности.</typeparam>
        /// <param name="newEntity">Новый объект связанной сущности.</param>
        /// <param name="currentEntity">Старый объект связанной сущности.</param>
        /// <returns>Объект связанной сущности.</returns>
        private T? UpdateRelatedEntity<T>(T? newEntity, T? currentEntity) where T : class, new()
        {
            if (newEntity != null)
                currentEntity ??= new T();
            else if (currentEntity != null)
            {
                _db.Set<T>().Remove(currentEntity);
                currentEntity = null;
            }

            return currentEntity;
        }
    }
}
