using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.ViewModels
{
    public class DeviceInfoViewModel
    {
        public string SerialNumber { get; set; }

        public string Humidity { get; set; }

        public string Temperature { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
