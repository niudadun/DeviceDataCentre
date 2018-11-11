using SensorAPIWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SensorAPIWeb.Domain.Configurations
{
    public class DeviceDetailsConfiguration : IEntityTypeConfiguration<DeviceDetails>
    {
        public void Configure(EntityTypeBuilder<DeviceDetails> builder)
        {

            builder.HasKey(k => k.DeviceDetailsId);

            builder.HasOne(d => d.Device)
                   .WithMany(p => p.DeviceDetails)
                   .HasForeignKey(d => d.DeviceId);
        }
    }
}
