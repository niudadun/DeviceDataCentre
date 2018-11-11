using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SensorAPIWeb.Services;
using SensorAPIWeb.ViewModels;

namespace SensorAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceUpdateController : ControllerBase
    {
        private readonly IDeviceUpdateRepository _deviceUpdateRepository;
        private readonly IGetDeviceDetailsRepository _getDeviceDetailsRepository;

        public DeviceUpdateController(IDeviceUpdateRepository deviceUpdateRepository, IGetDeviceDetailsRepository getDeviceDetailsRepository)
        {
            _deviceUpdateRepository = deviceUpdateRepository;
            _getDeviceDetailsRepository = getDeviceDetailsRepository;
        }

        // POST: api/DeviceUpdate
        [HttpPost]
        public void Post([FromBody] DeviceUpdateViewModel deviceInfo)
        {
            var result = _deviceUpdateRepository.DeviceDetailsUpdate(deviceInfo);
        }

        // PUT: api/DeviceUpdate/
        [HttpPut]
        public void Put([FromBody] DeviceSwitchOnViewModel deviceInfo)
        {
            var result = _deviceUpdateRepository.DeviceSwtichOn(deviceInfo);
        }

        // GET: api/GetDeviceInfo
        [HttpGet("{serialNumber}", Name = "GetLatestDeviceInfo")]
        public IActionResult GetLatestDeviceInfo(string serialNumber)
        {
            var result = _getDeviceDetailsRepository.GetLatestDeviceDetails(serialNumber);
            return new ObjectResult(result);
        }

    }
}
