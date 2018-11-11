using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SensorAPIWeb.Services;

namespace SensorAPIWeb.Controllers
{
    public class DevicesDataCentreController : Controller
    {
        private readonly IFileLoadRepository _fileLoadRepository;
        public DevicesDataCentreController(IFileLoadRepository fileLoadRepository)
        {
            _fileLoadRepository = fileLoadRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadFiles(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = _fileLoadRepository.FileLoad(file);
                return Json(result);
            }
        }
    }
}