using Cinema.API.Models.PersonModels;

namespace Cinema.API.Repositories.IRepositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task UpdatePersonAndSaveAsync(Person newPerson);

        IQueryable<Person> GetPersonsQuery(string? search);

        IQueryable<Person> GetSortedPersonsQuery(IQueryable<Person> query, bool sort = true);
    }
}
