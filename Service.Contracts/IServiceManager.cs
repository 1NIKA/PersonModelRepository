namespace Service.Contracts;

public interface IServiceManager
{
    IPersonService PersonService { get; }
    IUploadService UploadService { get; }
}