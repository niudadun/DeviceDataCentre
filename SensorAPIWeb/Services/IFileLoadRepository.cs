using Microsoft.AspNetCore.Http;
using SensorAPIWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Services
{
    public interface IFileLoadRepository
    {
        /// <summary>
        /// CSv file loading for device token
        /// </summary>
        /// <param name="file">csv file</param>
        /// <returns></returns>
        FileLoadResponseViewModel FileLoad(IFormFile file);
    }
}
