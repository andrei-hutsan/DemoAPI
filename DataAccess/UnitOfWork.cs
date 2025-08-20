using DataAccess.Interfaces;

namespace DataAccess;
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IPersonRepository personRepository)
    {
        Persons = personRepository;
    }

    public IPersonRepository Persons { get; }
}
