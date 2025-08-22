using DataAccess.Interfaces;

namespace DataAccess;
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IPersonRepository personRepository, IDepartmentRepository departments)
    {
        Persons = personRepository;
        Departments = departments;
    }

    public IPersonRepository Persons { get; }

    public IDepartmentRepository Departments { get; }
}
