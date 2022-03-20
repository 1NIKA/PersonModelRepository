using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Persons.Presentation.Controllers;

[Route("api/{personId:int}/file-uploads")]
[ApiController]
public class FileUploadsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public FileUploadsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(int personId, [FromForm]FileUploadDto? files)
    {
        if (files?.File is null) return BadRequest("FileUpload object is null");
        if (files.File.Length <= 0) return BadRequest("files must be more than 0");

        await _serviceManager.UploadService.UploadFile(personId, files, true);
        return Ok("uploaded successfully");
    }
}