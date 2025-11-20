namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations;

using Domain.AggregatesModel.ProductAggregate;
using Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.AggregatesModel.CartAggregate;
using Microsoft.EntityFrameworkCore;

public class CartTypeConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("carts").HasKey(c => c.Id);

        builder.HasMany(c => c.Items)
            .WithOne(c => c.Cart)
            .HasForeignKey(c => c.CartId)
            .HasConstraintName("FK_Cart_CartItem")
            .IsRequired();

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Cart>(c => c.UserId)
            .HasConstraintName("FK_Cart_User")
            .IsRequired();
    }
}

public class CartItemTypeConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("cart_items").HasKey(c => c.Id);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(c => c.ProductId)
            .HasConstraintName("FK_CartItem_Product")
            .IsRequired();

        builder.Property(c => c.Quantity)
            .HasDefaultValue(0)
            .HasColumnType("int")
            .IsRequired();

    }
}