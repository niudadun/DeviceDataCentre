using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SensorAPIWeb.Services;

namespace SensorAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDeviceInfoController : ControllerBase
    {
        private readonly IGetDeviceDetailsRepository _getDeviceDetailsRepository;

        public GetDeviceInfoController(IGetDeviceDetailsRepository getDeviceDetailsRepository)
        {
            _getDeviceDetailsRepository = getDeviceDetailsRepository;
        }

        // GET: api/DeviceUpdate
        [HttpGet("{serialNumber}", Name = "GetDeviceHistory")]
        public async Task<IActionResult> GetDeviceHistory(string serialNumber)
        {
            var result = await _getDeviceDetailsRepository.GetDeviceUpdatedHistory(serialNumber);
            return new ObjectResult(result);
        }
    }
}
