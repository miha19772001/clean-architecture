namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations
{
    using Domain.AggregatesModel.ProductAggregate;
    using Domain.AggregatesModel.UserAggregate;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.AggregatesModel.CartAggregate;
    using Microsoft.EntityFrameworkCore;

    public class CartTypeConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.CartItems)
                .WithOne(e => e.Cart)
                .HasForeignKey(e => e.CartId)
                .HasConstraintName("FK_Cart_CartItem")
                .IsRequired();

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Cart>(e => e.UserId)
                .HasConstraintName("FK_Cart_User")
                .IsRequired();
        }
    }
    
    public class CartItemTypeConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne<Product>()
                .WithOne()
                .HasForeignKey<CartItem>(e => e.ProductId)
                .HasConstraintName("FK_CartItem_Product")
                .IsRequired();
            
            builder.HasOne(e => e.Cart)
                .WithMany(e => e.CartItems)
                .HasForeignKey(e => e.CartId)
                .HasConstraintName("FK_CartItem_Cart")
                .IsRequired();
        }
    }
}