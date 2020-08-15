namespace CarRentalSystem.Infrastructure.Persistence.Configurations
{
    using Domain.Models;
    using Domain.Models.Dealers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.HasKey(d => d.Id);

            builder
                .HasMany(d => d.CarAds)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("carAds");

            builder
                .HasMany(d => d.CarAds);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(ModelConstants.Dealers.MAX_LENGTH);

            builder
                .OwnsOne(
                    d => d.PhoneNumber,
                    p =>
                    {
                        p.WithOwner();
                        p.Property(pn => pn.Value);
                    });

        }
    }
}
