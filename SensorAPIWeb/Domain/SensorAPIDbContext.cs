using SensorAPIWeb.Domain.Entities;
using SensorAPIWeb.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAPIWeb.Domain
{
    public class SensorAPIDbContext : DbContext
    {
        public SensorAPIDbContext(DbContextOptions<SensorAPIDbContext> options) 
            : base(options)
        {    
        }
        public DbSet<Device> Devices { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<DeviceDetails> DeviceDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
