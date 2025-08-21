using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Data;
public class PersonRepository : IPersonRepository
{
    private readonly ISqlDataAccess _db;

    public PersonRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<PersonModel>> GetAllAsync() =>
        _db.LoadData<PersonModel, dynamic>("dbo.spPerson_GetAll", new { });

    public async Task<PersonModel?> GetByIdAsync(Guid id)
    {
        var result = await _db.LoadData<PersonModel, dynamic>("dbo.spPerson_Get", new { Id = id });
        return result.FirstOrDefault();
    }

    public Task AddAsync(PersonModel entity) =>
        _db.SaveData("dbo.spPerson_Insert", new { FirstName = entity.Firstname, Lastname = entity.Lastname, Email = entity.Email});

    public Task UpdateAsync(PersonModel entity) =>
        _db.SaveData("dbo.spPerson_Update", entity);

    public Task DeleteAsync(Guid id) =>
        _db.SaveData("dbo.spPerson_Delete", new { Id = id });
}
