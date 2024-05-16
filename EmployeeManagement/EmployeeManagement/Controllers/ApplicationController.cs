using EmployeeManagement.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("api/[controller]/upload-file")]
        public IActionResult UploadFile(List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return BadRequest();
            }

            string dir = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");

            foreach (var file in files)
            {
                string filePath = Path.Combine(dir, file.FileName);
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return Ok("Upload file successfully");
        }
    }
}
