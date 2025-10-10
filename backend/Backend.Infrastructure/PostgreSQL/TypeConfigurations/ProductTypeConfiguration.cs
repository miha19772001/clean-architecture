namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations
{
    using Domain.AggregatesModel.FileAggregate;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.AggregatesModel.ProductAggregate;

    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne<Image>()
                .WithOne()
                .HasForeignKey<Product>(e => e.ImageId)
                .HasConstraintName("FK_Product_Image")
                .IsRequired();
        }
    }
}