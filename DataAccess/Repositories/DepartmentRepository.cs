using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories;
public class DepartmentRepository : IDepartmentRepository
{
    private readonly ISqlDataAccess _db;
    public DepartmentRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task AddAsync(DepartmentModel entity)
        => _db.SaveData("dbo.spDepartment_Insert",
         new { Firstname = entity.Name });


    public Task DeleteAsync(Guid id)
        => _db.SaveData("dbo.spDepartment_Delete", new {Id = id });

    public Task<IEnumerable<DepartmentModel>> GetAllAsync()
        => _db.LoadData<DepartmentModel, dynamic>("dbo.spDepartment_GetAll", new { });

    public async Task<DepartmentModel?> GetByIdAsync(Guid id)
    {
        var result = await _db.LoadData<DepartmentModel, dynamic>("dbo.spDepartment_Get", new { Id = id });
        return result.FirstOrDefault();
    }

    public Task UpdateAsync(DepartmentModel entity)
        => _db.SaveData("dbo.spDepartment_Update", new { Id = entity.Id,  Name = entity.Name });
}
