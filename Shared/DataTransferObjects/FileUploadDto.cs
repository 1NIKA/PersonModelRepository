using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects;

public record FileUploadDto(IFormFile File);