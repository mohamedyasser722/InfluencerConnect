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
    }
}
