using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAPIWeb.Domain.Entities
{
    public class DeviceDetails
    {
        public int DeviceDetailsId { get; set; }

        public string SerialNumber { get; set; }

        public string Humidity { get; set; }

        public string Temperature { get; set; }

        public DateTime UpdateTime { get; set; }

        public int? DeviceId { get; set; }

        public Device Device { get; set; }
    }
}
