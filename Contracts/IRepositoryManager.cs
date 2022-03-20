namespace Contracts;

public interface IRepositoryManager
{
    IPersonRepository Person { get; }
    Task Save();
}