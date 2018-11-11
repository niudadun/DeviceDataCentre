using SensorAPIWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SensorAPIWeb.Domain.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(e => e.SerialNumber).IsRequired().HasMaxLength(24);

            builder.HasOne(t => t.Token)
                .WithOne(d => d.Device)
                .HasForeignKey<Token>(t => t.TypeId);
        }
    }
}
