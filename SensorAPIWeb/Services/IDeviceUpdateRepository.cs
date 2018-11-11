using SensorAPIWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Services
{
    public interface IDeviceUpdateRepository
    {
        /// <summary>
        /// Update device's temperature, humidity in database.
        /// </summary>
        /// <param >device serial number, Humidity, Temperature</param>
        /// <returns></returns>
        Task DeviceDetailsUpdate(DeviceUpdateViewModel deviceDetails);

        /// <summary>
        /// Update device info in database when switch on .
        /// </summary>
        /// <param >device serial number, BatteryVoltage, FirmwareVersion</param>
        /// <returns></returns>
        Task DeviceSwtichOn(DeviceSwitchOnViewModel deviceInfo);
    }
}
