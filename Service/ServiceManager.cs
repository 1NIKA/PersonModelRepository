using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IPersonService> _personService;
    private readonly Lazy<IUploadService> _uploadService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _personService = new Lazy<IPersonService>(() => 
            new PersonService(repositoryManager, logger, mapper));
        _uploadService = new Lazy<IUploadService>(() => 
            new UploadService(repositoryManager, logger, mapper));
    }

    public IPersonService PersonService => _personService.Value;
    public IUploadService UploadService => _uploadService.Value;
}