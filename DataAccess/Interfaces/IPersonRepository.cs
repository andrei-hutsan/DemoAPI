using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Interfaces;
public interface IPersonRepository : IGenericRepository<PersonModel>
{
    Task<IEnumerable<PersonWithDepDto>> GetAllDeatilsAsync();
    Task<PersonWithDepDto?> GetDetailsByIdAsync(Guid id);
}
