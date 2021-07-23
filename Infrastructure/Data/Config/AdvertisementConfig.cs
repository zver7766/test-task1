using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class AdvertisementConfig : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Cost).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
            
        }
    }
}
