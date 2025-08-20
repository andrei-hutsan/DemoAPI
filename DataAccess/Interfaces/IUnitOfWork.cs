namespace DataAccess.Interfaces;
public interface IUnitOfWork
{
    IPersonRepository Persons { get; }
}
