using Microsoft.EntityFrameworkCore;
using SensorAPIWeb.Services;
using SensorAPIWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Domain.DBRepositories
{
    public class GetDeviceDetailsRepository : IGetDeviceDetailsRepository
    {
        private readonly SensorAPIDbContext _deviceUpatedBcontext;
        public GetDeviceDetailsRepository(SensorAPIDbContext deviceUpatedBcontext)
        {
            _deviceUpatedBcontext = deviceUpatedBcontext;
        }
        public async Task<List<DeviceInfoViewModel>> GetDeviceUpdatedHistory(string serialNumber)
        {
            List<DeviceInfoViewModel> response = new List<DeviceInfoViewModel>();
            try
            {
                var result = _deviceUpatedBcontext.DeviceDetails
                    .Where(i => i.SerialNumber == serialNumber)
                    .Select(dt => new DeviceInfoViewModel()
                    {
                        SerialNumber = dt.SerialNumber,
                        Humidity = dt.Humidity,
                        Temperature = dt.Temperature,
                        UpdateTime = dt.UpdateTime
                    });
                if (result.Any())
                {
                    response = await result.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public DeviceInfoViewModel GetLatestDeviceDetails(string serialNumber)
        {
            DeviceInfoViewModel response = new DeviceInfoViewModel();
            try
            {
                var result = _deviceUpatedBcontext.DeviceDetails
                    .Where(i => i.SerialNumber == serialNumber)
                    .OrderByDescending(t => t.UpdateTime)
                    .FirstOrDefault();
                if (result != null)
                {
                    response.SerialNumber = result.SerialNumber;
                    response.Humidity = result.Humidity;
                    response.Temperature = result.Temperature;
                    response.UpdateTime = result.UpdateTime;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
