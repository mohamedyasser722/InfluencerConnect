using InfluencerConnect.Domain.Influencers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfluencerConnect.Infrastructure.Configurations;
public class InfluencerConfigurations : IEntityTypeConfiguration<Influencer>
{
    public void Configure(EntityTypeBuilder<Influencer> builder)
    {
        // Define the table name
        builder.ToTable("Influencers");

        // Primary Key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever(); 

        // Relationships
        builder.HasOne(i => i.ApplicationUser)
            .WithOne()
            .HasForeignKey<Influencer>(i => i.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Properties
        builder.Property(i => i.ApplicationUserId)
            .IsRequired();

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

        // Indexes (Optional for faster lookups)
        builder.HasIndex(i => i.ApplicationUserId).IsUnique(true);
    }
}
