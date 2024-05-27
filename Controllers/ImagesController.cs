using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Model.Domain;
using NZWalk.API.Model.DTO;
using NZWalk.API.Respositories;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //User repository to upload img
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };
                //User repository to upload image
                await _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowExtension = new string[] { ".jpg", ".jped", ".png" };
            if (
                !allowExtension.Any(ext =>
                    ext.Equals(
                        Path.GetExtension(request.File.FileName),
                        StringComparison.OrdinalIgnoreCase
                    )
                )
            )
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError(
                    "file",
                    "File size more than 10MB, please upload a smaller size"
                );
            }
        }
    }
}
