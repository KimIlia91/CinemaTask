using AutoMapper;
using Cinema.API.Models.PersonModels.PersonDtos;
using Cinema.API.Models.PersonModels;
using Cinema.API.Repositories.IRepositories;
using Cinema.API.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Cinema.API.Models;

namespace Cinema.API.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор класса <see cref="PersonRepository"/>.
        /// </summary>
        /// <param name="db">Контекст базы данных <see cref="ApplicationDbContext"/>.</param>
        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<Page<PersonDto>> GetPageOfPersonsAsync(PersonsFilterRequest opt)
        {
            var query = _unitOfWork.Persons.GetPersonsQuery(opt.Search);
            query = _unitOfWork.Persons.GetSortedPersonsQuery(query, opt.Sort);
            var totalPersons = await _unitOfWork.Persons.GetTotalCountOfEntity(query);
            var personsPage = await _unitOfWork.Persons.GetEntitiesForPageAsync(query, opt.Page, opt.PageSize);
            var personDtos = _mapper.Map<IEnumerable<PersonDto>>(personsPage);
            return new Page<PersonDto>(personDtos, opt.Page, opt.PageSize, totalPersons);
        }

        /// <inheritdoc/>
        public async Task CreatePersonAsync(PersonCreateDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            var personsExist = await _unitOfWork.Persons.GetOrDefaultAsync(p => p.LastName == person.LastName &&
                                                                                p.FirstName == person.FirstName);
            if (personsExist != null)
                throw new ArgumentException("Персона с таким именем и фамилией уже существует.");

            await _unitOfWork.Persons.AddAndSaveAsync(person);
        }

        /// <inheritdoc/>
        public async Task<PersonDto> GetPersonByIdAsync(Guid id)
        {
            var person = await _unitOfWork.Persons.GetOrDefaultAsync(p => p.Id == id,
                                                                     includeProperty: "Actor,Director,Screenwriter");
            if (person is null)
                throw new ArgumentException("Персона не найдена.");

            return _mapper.Map<PersonDto>(person);
        }

        /// <inheritdoc/>
        public async Task UpdatePersonAsync(PersonDto personDto)
        {
            var personToUpdate = await _unitOfWork.Persons.GetOrDefaultAsync(p => p.Id == personDto.Id);

            if (personToUpdate is null)
                throw new ArgumentNullException(nameof(personDto), "Персона не существует.");

            personToUpdate = _mapper.Map<Person>(personDto);

            if (!await IsPersonUniqueAsync(personToUpdate))
                throw new ArgumentException("Персона с таким именем и фамилией уже существует.");

            await _unitOfWork.Persons.UpdatePersonAndSaveAsync(personToUpdate);
        }

        /// <inheritdoc/>
        public async Task DeletePersonAsync(Guid id)
        {
            var person = await _unitOfWork.Persons.GetOrDefaultAsync(p => p.Id == id);

            if (person is null)
                throw new ArgumentNullException(nameof(id), "Персона с таким идентификационным номером не найден.");

            await _unitOfWork.Persons.DeleteAndSaveAsync(person);
        }

        /// <summary>
        /// Проверяет, является ли переданный объект Person уникальным в базе данных.
        /// </summary>
        /// <param name="person">Объект Person для проверки уникальности.</param>
        /// <returns>Значение true, если объект Person является уникальным; в противном случае - false.</returns>
        /// <remarks>
        /// Метод использует имя и фамилию объекта Person для проверки его уникальности.
        /// </remarks>
        private async Task<bool> IsPersonUniqueAsync(Person person)
        {
            var isUniquePerson = await _unitOfWork.Persons.GetAllEntities(p => p.FirstName.Contains(person.FirstName) &&
                                                                               p.LastName.Contains(person.LastName))
                                                          .ToListAsync();
            return isUniquePerson.Count == 1;
        }
    }
}
