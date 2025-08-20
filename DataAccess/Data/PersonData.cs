using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;
public class PersonData : IPersonData
{
    private readonly ISqlDataAccess _db;

    public PersonData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<PersonModel>> GetPersons() =>
        _db.LoadData<PersonModel, dynamic>("dbo.spPerson_GetAll", new { });

    public async Task<PersonModel?> GetPerson(Guid id)
    {
        var result = await _db.LoadData<PersonModel, dynamic>("dbo.spPerson_Get", new { Id = id });
        return result.FirstOrDefault();
    }

    public Task InsertPerson(PersonModel person) =>
        _db.SaveData("dbo.spPerson_Insert", person);

    public Task UpdatePerson(PersonModel person) =>
        _db.SaveData("dbo.spPerson_Update", person);

    public Task DeletePerson(Guid id) =>
        _db.SaveData("dbo.spPerson_Delete", new { Id = id });
}
