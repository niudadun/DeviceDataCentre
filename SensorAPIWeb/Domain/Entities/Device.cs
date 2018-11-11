using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAPIWeb.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public string BatteryVoltage { get; set; }

        public string FirmwareVersion { get; set; }

        public DateTime UpdateTime { get; set; }

        public Token Token { get; set; }

        public ICollection<DeviceDetails> DeviceDetails { get; set; }

    }
}
