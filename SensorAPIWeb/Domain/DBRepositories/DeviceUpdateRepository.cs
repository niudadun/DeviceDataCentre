using SensorAPIWeb.Domain.Entities;
using SensorAPIWeb.Models;
using SensorAPIWeb.Services;
using SensorAPIWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Domain.DBRepositories
{
    public class DeviceUpdateRepository : IDeviceUpdateRepository
    {
        private readonly SensorAPIDbContext _deviceUpatedBcontext;

        public DeviceUpdateRepository(SensorAPIDbContext sensorAPIdBContext)
        {
            _deviceUpatedBcontext = sensorAPIdBContext;
        }

        public async Task DeviceDetailsUpdate(DeviceUpdateViewModel deviceDetails)
        {
            try
            {
                var deviceinfo = new DeviceDetails()
                {
                    SerialNumber = deviceDetails.SerialNumber,
                    Humidity = deviceDetails.Humidity,
                    Temperature = deviceDetails.Temperature,
                    UpdateTime = DateTime.Now,
                };
                await _deviceUpatedBcontext.DeviceDetails.AddAsync(deviceinfo);
                _deviceUpatedBcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeviceSwtichOn(DeviceSwitchOnViewModel deviceInfo)
        {
            try
            {
                var device = _deviceUpatedBcontext.Devices.Where(d => d.SerialNumber == deviceInfo.SerialNumber).FirstOrDefault();
                device.BatteryVoltage = deviceInfo.BatteryVoltage;
                device.FirmwareVersion = deviceInfo.FirmwareVersion;
                device.UpdateTime = DateTime.Now;
                await _deviceUpatedBcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
    }
}
