using SensorAPIWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SensorAPIWeb.Domain.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(e => e.TokenNumber).HasMaxLength(64);

        }
    }
}
