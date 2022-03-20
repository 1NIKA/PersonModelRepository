using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IPersonRepository> _personRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _personRepository = new Lazy<IPersonRepository>(() => new PersonRepository(repositoryContext));
    }

    public IPersonRepository Person => _personRepository.Value;
    
    public async Task Save() => await _repositoryContext.SaveChangesAsync();
}