using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace HospitalDB.Configuration
{
	public class BuildingConfig : IEntityTypeConfiguration<Building>
	{
		public void Configure(EntityTypeBuilder<Building> builder)
		{
            
            builder.HasKey(e => e.IdBuilding);
            builder.Property(e => e.IdBuilding).ValueGeneratedOnAdd();
            builder.Property(e => e.Street).HasMaxLength(100).IsRequired();
            builder.Property(e => e.StreetNumber).IsRequired();
            builder.Property(e => e.City).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Height).HasColumnType("decimal(6, 2)");
            builder.ToTable("Building");

            builder.HasMany(building => building.StartCampaigns)
                      .WithOne(campaign => campaign.FromBuilding)
                      .HasForeignKey(campaign => campaign.FromIdBuilding)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();

            builder.HasMany(building => building.EndCampaigns)
                      .WithOne(campaign => campaign.ToBuilding)
                      .HasForeignKey(campaign => campaign.ToIdBuilding)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();

        }
	}
}
