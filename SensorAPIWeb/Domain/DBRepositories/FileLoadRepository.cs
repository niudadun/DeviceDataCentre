using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SensorAPIWeb.Services;
using SensorAPIWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SensorAPIWeb.Domain.DBRepositories
{
    public class FileLoadRepository : IFileLoadRepository
    {
        private readonly SensorAPIDbContext _fileLoaddBcontext;
        public FileLoadRepository(SensorAPIDbContext fileLoaddBcontext)
        {
            _fileLoaddBcontext = fileLoaddBcontext;
        }
        public FileLoadResponseViewModel FileLoad(IFormFile file)
        {
            var result = new FileLoadResponseViewModel();
            try
            {
                TextReader reader = new StreamReader(file.OpenReadStream());
                var csvReader = new CsvReader(reader);
                var serialNumber_token_Pair_List = csvReader.GetRecords<DeviceCSVLoadViewModel>().ToList();
                if (serialNumber_token_Pair_List.Count > 0)
                {
                    foreach (var serialNumber_token_Pair in serialNumber_token_Pair_List)
                    {
                        SqlParameter serialNumber = new SqlParameter("@SerialNumber", serialNumber_token_Pair.SerialNumber);
                        SqlParameter Token = new SqlParameter("@Token", serialNumber_token_Pair.Token);
                        _fileLoaddBcontext.Database.ExecuteSqlCommand("InsertOrUpdateToken @SerialNumber, @Token", serialNumber, Token);
                    }
                }
                result.Message = "Upload successfully";
            }
            catch (Exception ex)
            {
                result.Message = "Upload fail";
            }
            
            return result;
        }
    }
}
