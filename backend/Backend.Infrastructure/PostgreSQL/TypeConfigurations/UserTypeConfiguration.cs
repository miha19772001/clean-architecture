namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.AggregatesModel.UserAggregate;

    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ComplexProperty(e => e.Role, b =>
            {
                b.IsRequired();
                b.Property(p => p.Value).HasColumnName("Role");
            });
        }
    }
}
