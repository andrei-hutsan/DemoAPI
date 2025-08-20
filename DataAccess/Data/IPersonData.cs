using DataAccess.Models;

namespace DataAccess.Data;
public interface IPersonData
{
    Task DeletePerson(Guid id);
    Task<PersonModel?> GetPerson(Guid id);
    Task<IEnumerable<PersonModel>> GetPersons();
    Task InsertPerson(PersonModel person);
    Task UpdatePerson(PersonModel person);
}