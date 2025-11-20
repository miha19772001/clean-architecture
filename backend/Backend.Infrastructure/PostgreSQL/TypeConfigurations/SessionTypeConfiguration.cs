namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations;

using Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.SessionAggregate;

public class SessionTypeConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions").HasKey(s => s.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .HasConstraintName("FK_Sessions_Users")
            .IsRequired();

        builder.Property(s => s.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(s => s.Ip)
            .HasDefaultValue("")
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.Agent)
            .HasDefaultValue("")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnType("timestamp(0) without time zone")
            .HasDefaultValueSql("NOW()")
            .IsRequired();
    }
}