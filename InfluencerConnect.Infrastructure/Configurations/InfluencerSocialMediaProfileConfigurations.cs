using InfluencerConnect.Domain.Influencers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Configurations;
public class InfluencerSocialMediaProfileConfigurations : IEntityTypeConfiguration<InfluencerSocialMediaProfile>
{
    public void Configure(EntityTypeBuilder<InfluencerSocialMediaProfile> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.InfluencerId)
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

        builder.HasOne<Influencer>()
            .WithMany(x => x.InfluencerSocialMediaProfiles)
            .HasForeignKey(x => x.InfluencerId);
    }
}
