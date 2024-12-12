using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using InfluencerConnect.Domain.Influencers;
using InfluencerConnect.Domain.Brands;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
        .HasOne(e => e.Influencer)
        .WithOne(e => e.ApplicationUser)
        .HasForeignKey<Influencer>(e => e.Id)
        .IsRequired();

        builder
        .HasOne(e => e.Brand)
        .WithOne(e => e.ApplicationUser)
        .HasForeignKey<Brand>(e => e.Id)
        .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.UserType)
         .HasConversion(
             v => v.ToString(),  // Convert enum to string for the database
             v => Enum.Parse<UserType>(v))
         .HasMaxLength(50); // Convert string to enum for the application

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(u => u.IsAccepted)
        .HasDefaultValue(false)
        .IsRequired();


        builder.OwnsMany(u => u.RefreshTokens, refreshToken =>
        {
            // Configure the primary key for RefreshToken
            refreshToken.HasKey(rt => rt.Token);

            // Map RefreshToken properties to columns
            refreshToken.Property(rt => rt.Token)
                        .IsRequired()
                        .HasMaxLength(500); // Adjust length if needed

            refreshToken.Property(rt => rt.ExpiresOn)
                        .IsRequired();

            refreshToken.Property(rt => rt.CreatedOn)
                        .IsRequired();

            refreshToken.Property(rt => rt.RevokedOn)
                        .IsRequired(false); // Nullable

            // Configure the table name for the owned type
            refreshToken.ToTable("RefreshTokens");

            // Map foreign key relationship
            refreshToken.WithOwner()
                        .HasForeignKey("ApplicationUserId"); // Optional if using conventions

            // Index for faster queries (important for scenarios with multiple devices)
            refreshToken.HasIndex("ApplicationUserId");
        });
    }
}
