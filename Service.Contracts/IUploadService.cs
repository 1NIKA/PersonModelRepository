using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IUploadService
{
    Task UploadFile(int personId, FileUploadDto files, bool trackChanges);
}