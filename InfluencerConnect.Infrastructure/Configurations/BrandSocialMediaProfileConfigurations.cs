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
public class BrandSocialMediaProfileConfigurations : IEntityTypeConfiguration<BrandSocialMediaProfile>
{
    public void Configure(EntityTypeBuilder<BrandSocialMediaProfile> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.BrandId)
            .IsRequired();

        builder.Property(x => x.ProfileLink)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.PlatformName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.AccountUserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne<Brand>()
            .WithMany(x => x.BrandSocialMediaProfile)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
