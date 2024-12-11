using InfluencerConnect.Domain.Influencers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace InfluencerConnect.Infrastructure.Configurations;
public class InfluencerConfigurations : IEntityTypeConfiguration<Influencer>
{
    public void Configure(EntityTypeBuilder<Influencer> builder)
    {
        // Define the table name
        builder.ToTable("Influencers");

        builder
       .HasOne(i => i.ApplicationUser) // Influencer has one ApplicationUser
       .WithOne(a => a.Influencer) // ApplicationUser has one Influencer
       .HasForeignKey<Influencer>(i => i.Id) // Foreign key is the same as Id (primary key)
       .IsRequired(); // The relationship is required

        builder.OwnsOne(i => i.PriceRange, pr =>
        {
            pr.Property(p => p.MinPrice)
                .HasColumnName("MinPrice")
                .HasPrecision(18, 2)
                .IsRequired();

            pr.Property(p => p.MaxPrice)
                .HasColumnName("MaxPrice")
                .HasPrecision(18, 2)
                .IsRequired();
        });

        builder.Property(i => i.Bio)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(i => i.ProfilePictureUrl)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.OwnsOne(influencer => influencer.BirthDate, owned =>
        {
            owned.Property(b => b.Value)
                .HasColumnName("BirthDate") // Maps to a single column named "BirthDate"
                .HasConversion(
                    dateOnly => dateOnly.ToString("yyyy-MM-dd"), // Convert DateOnly to string for database storage
                    value => DateOnly.Parse(value)               // Convert string back to DateOnly
                )
                .IsRequired(); // Ensure BirthDate is required
        });


        builder
            .HasMany(x => x.InfluencerSocialMediaProfiles)
            .WithOne()
            .HasForeignKey(x => x.InfluencerId);

        builder.Property(i => i.Gender)
            .HasConversion(
                v => v.ToString(),  // Convert enum to string for the database
                v => Enum.Parse<Gender>(v)); // Convert string to enum for the application
    }
}
