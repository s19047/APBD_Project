using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace HospitalDB.Configuration
{
	public class BannerConfig : IEntityTypeConfiguration<Banner>
	{
		public void Configure(EntityTypeBuilder<Banner> builder)
		{
            builder.HasKey(e => e.IdAdvertisment);
            builder.Property(e => e.IdAdvertisment).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(6, 2)");
            builder.Property(e => e.IdCampaign).IsRequired();
            builder.Property(e => e.Area).HasColumnType("decimal(6, 2)");
            builder.ToTable("Banner");

        }
	}
}
