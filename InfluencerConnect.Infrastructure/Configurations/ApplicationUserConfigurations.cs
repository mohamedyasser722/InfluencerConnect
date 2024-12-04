using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.UserType)
         .HasConversion(
             v => v.ToString(),  // Convert enum to string for the database
             v => Enum.Parse<UserType>(v)); // Convert string to enum for the application

        builder.Property(u => u.IsAccepted)
        .HasDefaultValue(false)
        .IsRequired();
    }
}
