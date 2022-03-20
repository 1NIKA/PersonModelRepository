using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class UploadService : IUploadService
{
    private const string PATH = "../WebAPI/wwwroot/uploadFiles/";
    
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public UploadService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task UploadFile(int personId, FileUploadDto files, bool trackChanges)
    {
        var person = await _repositoryManager.Person.GetPerson(personId, trackChanges);
        if (person is null) throw new PersonNotFoundException(personId);

        var path = await UploadFileInFileSystem(files);

        person.Image ??= new Image();
        person.Image.Name = files.File.FileName;
        person.Image.Path = path;
        await _repositoryManager.Save();
    }

    private async Task<string> UploadFileInFileSystem(FileUploadDto files)
    {
        if (!Directory.Exists(PATH)) Directory.CreateDirectory(PATH);

        var filePath = PATH + files.File.FileName;
        await using var fileStream = File.Create(filePath);
        await files.File.CopyToAsync(fileStream);
        await fileStream.FlushAsync();

        return filePath;
    }
}