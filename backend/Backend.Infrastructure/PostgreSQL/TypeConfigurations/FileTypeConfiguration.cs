namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.FileAggregate;

public class FileTypeConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("files").HasKey(f => f.Id);

        builder.Property(f => f.CreatedAt)
            .HasColumnType("timestamp(0) without time zone")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        builder.Property(f => f.OriginalName)
            .HasDefaultValue("")
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(f => f.FileSystemName)
            .HasDefaultValue("")
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();

        builder.ComplexProperty(f => f.Type, b =>
        {
            b.IsRequired();
            b.Property(ft => ft.Value).HasColumnName("Type");
        });
    }
}