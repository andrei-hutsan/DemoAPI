using DataAccess.DTO;
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

    public Task AddAsync(PersonModel entity)
     => _db.SaveData("dbo.spPerson_Insert",
         new { Firstname = entity.Firstname, Lastname = entity.Lastname, Email = entity.Email, DepartmentId = entity.DepartmentId });

    public Task UpdateAsync(PersonModel entity) =>
        _db.SaveData("dbo.spPerson_Update", entity);

    public Task DeleteAsync(Guid id) =>
        _db.SaveData("dbo.spPerson_Delete", new { Id = id });

    public Task<IEnumerable<PersonWithDepDto>> GetAllDeatilsAsync()
        =>_db.LoadData<PersonWithDepDto, dynamic>("dbo.spPerson_GetAllWithDepartment", new { });

    public async Task<PersonWithDepDto?> GetDetailsByIdAsync(Guid id)
    {
        var result = await _db.LoadData<PersonWithDepDto, dynamic>("dbo.spPerson_GetWithDepartment", new {Id = id});
        return result.FirstOrDefault();
    }
}
