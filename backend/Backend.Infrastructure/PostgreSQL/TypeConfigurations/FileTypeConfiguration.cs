namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.AggregatesModel.FileAggregate;

    public class FileTypeConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ComplexProperty(e => e.Type, b =>
            {
                b.IsRequired();
                b.Property(p => p.Value).HasColumnName("Type");
            });
        }
    }
}
