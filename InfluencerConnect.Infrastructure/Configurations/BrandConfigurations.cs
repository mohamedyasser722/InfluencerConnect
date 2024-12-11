using InfluencerConnect.Domain.ApplicationUsers;
using InfluencerConnect.Domain.Brands;
using InfluencerConnect.Domain.Influencers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Configurations;
public class BrandConfigurations : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder
       .HasOne(i => i.ApplicationUser) // Influencer has one ApplicationUser
       .WithOne(a => a.Brand) // ApplicationUser has one Influencer
       .HasForeignKey<Brand>(i => i.Id) // Foreign key is the same as Id (primary key)
       .IsRequired(); // The relationship is required

        builder.OwnsOne(brand => brand.BrandInfo, brandInfo =>
        {
            brandInfo.Property(info => info.Name)
                .HasMaxLength(100)
                .IsRequired();
            brandInfo.Property(info => info.Description)
                .HasMaxLength(500);
            brandInfo.Property(info => info.WebsiteUrl)
                .HasMaxLength(100);
            brandInfo.Property(info => info.LogoUrl)
                .HasMaxLength(500);
        });

        builder.HasMany(brand => brand.BrandSocialMediaProfile)
            .WithOne()
            .HasForeignKey(socialMediaProfile => socialMediaProfile.BrandId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
