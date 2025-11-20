namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations;

using Domain.AggregatesModel.FileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.ProductAggregate;

public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products").HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasDefaultValue("")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(p => p.Description)
            .HasDefaultValue("")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(p => p.Price)
            .HasDefaultValue(0)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Quantity)
            .HasDefaultValue(0)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp(0) without time zone")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        builder.HasOne<File>()
            .WithOne()
            .HasForeignKey<Product>(p => p.ImageId)
            .HasConstraintName("FK_Product_Image")
            .IsRequired();
    }
}