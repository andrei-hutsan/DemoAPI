namespace DataAccess.Interfaces;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string conectionId = "Default");
    Task SaveData<T>(string storedProcedure, T parameters, string conectionId = "Default");
}