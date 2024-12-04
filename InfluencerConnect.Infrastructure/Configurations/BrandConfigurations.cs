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

        builder.HasKey(brand => brand.Id);

        builder.Property(brand => brand.Id)
            .ValueGeneratedNever();

        // Relationships
        builder.HasOne(i => i.ApplicationUser)
            .WithOne()
            .HasForeignKey<Brand>(i => i.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

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

        builder.HasIndex(i => i.ApplicationUserId).IsUnique(true);
    }
}
