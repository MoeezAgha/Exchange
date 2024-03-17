using AutoMapper;
using Exchange.BAL.Services.Contracts;
using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [DisableRequestSizeLimit]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("MultipleUpload")]
        public IActionResult MultipleUpload(IFormFile[] formFiles)
        {
            foreach (var item in formFiles)
            {
                UploadFile(item).Wait();
            }

            return StatusCode(200);
        }

        // Make the method private if it's only used within this controller
        private async Task UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\Upload";
                var uploadPath = _webHostEnvironment.WebRootPath + imagePath;
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fullPath = Path.Combine(uploadPath, file.FileName); // Use FileName instead of Name
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }
    }
}

