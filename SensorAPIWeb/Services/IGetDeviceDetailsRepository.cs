using SensorAPIWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Services
{
    public interface IGetDeviceDetailsRepository
    {
        /// <summary>
        /// Get all records of device's temperature, humidity history in database.
        /// </summary>
        /// <param >device serial number</param>
        /// <returns>List of Device details</returns>
        Task<List<DeviceInfoViewModel>> GetDeviceUpdatedHistory(string serialNumber);

        /// <summary>
        /// Get latest updated device details.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns>latest updated device details</returns>
        DeviceInfoViewModel GetLatestDeviceDetails(string serialNumber);
    }
}
