using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace HospitalDB.Configuration
{
	public class CampaignConfig : IEntityTypeConfiguration<Campaign>
	{
		public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(e => e.IdCampaign);
            builder.Property(e => e.IdCampaign).ValueGeneratedOnAdd();
            builder.Property(e => e.IdUser).IsRequired();
            builder.Property(e => e.PricePerSquareMeter).HasColumnType("decimal(6,2)");
            builder.Property(e => e.FromIdBuilding).IsRequired();
            builder.Property(e => e.ToIdBuilding).IsRequired();
            builder.ToTable("Campaign");

            builder.HasMany(campaign => campaign.Banners)
                     .WithOne(banner => banner.Campaign)
                     .HasForeignKey(banner => banner.IdCampaign)
                     .IsRequired();
                       
        }
    }

}
