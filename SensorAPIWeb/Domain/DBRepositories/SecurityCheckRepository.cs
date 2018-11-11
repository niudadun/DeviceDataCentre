using SensorAPIWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Domain.DBRepositories
{
    public class SecurityCheckRepository : ISecurityCheckRepository
    {
        private readonly SensorAPIDbContext _deviceUpatedBcontext;
        public SecurityCheckRepository(SensorAPIDbContext deviceUpatedBcontext)
        {
            _deviceUpatedBcontext = deviceUpatedBcontext;
        }
        public bool TokenValidation(string token)
        {
            var tokenValid = false;
            var result = _deviceUpatedBcontext.Tokens.Where(i => i.TokenNumber == token).FirstOrDefault();
            if (result != null)
            {
                tokenValid = true;
            }
            return tokenValid;
        }
    }
}
