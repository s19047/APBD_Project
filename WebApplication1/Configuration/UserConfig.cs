using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace HospitalDB.Configuration
{
	public class UserConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{

            builder.HasKey(e => e.IdUser);
            builder.Property(e => e.IdUser).ValueGeneratedOnAdd();
            builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Phone).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Login).HasMaxLength(100).IsRequired();
            builder.ToTable("User");

            builder.HasMany(user => user.Campaigns)
                   .WithOne(campaign => campaign.User)
                   .HasForeignKey(campaign => campaign.IdUser)
                   .IsRequired();


        }
    }
}
