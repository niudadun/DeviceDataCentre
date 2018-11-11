using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAPIWeb.Domain.Entities
{
    public class Token
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string TokenNumber { get; set; }

        public Device Device { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
