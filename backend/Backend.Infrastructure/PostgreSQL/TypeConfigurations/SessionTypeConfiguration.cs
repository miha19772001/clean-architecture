namespace Backend.Infrastructure.PostgreSQL.TypeConfigurations
{
    using Domain.AggregatesModel.UserAggregate;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.AggregatesModel.SessionAggregate;

    public class SessionTypeConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_Session_User")
                .IsRequired();
        }
    }
}