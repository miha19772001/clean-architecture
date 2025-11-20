namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.UserAggregate;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users").HasKey(u => u.Id);

        builder.Property(u => u.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(u => u.Login)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.ComplexProperty(u => u.Role, b =>
        {
            b.Property(p => p.Value)
                .HasDefaultValue("User")
                .HasColumnName("Role")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Property(s => s.CreatedAt)
            .HasColumnType("timestamp(0) without time zone")
            .HasDefaultValueSql("NOW()")
            .IsRequired();
    }
}