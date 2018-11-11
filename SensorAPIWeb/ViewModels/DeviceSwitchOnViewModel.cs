using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.ViewModels
{
    public class DeviceSwitchOnViewModel
    {
        public string SerialNumber { get; set; }

        public string BatteryVoltage { get; set; }

        public string FirmwareVersion { get; set; }
    }
}
