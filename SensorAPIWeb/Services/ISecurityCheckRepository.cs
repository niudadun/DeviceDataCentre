using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Services
{
    public interface ISecurityCheckRepository
    {
        bool TokenValidation(string token);
    }
}
