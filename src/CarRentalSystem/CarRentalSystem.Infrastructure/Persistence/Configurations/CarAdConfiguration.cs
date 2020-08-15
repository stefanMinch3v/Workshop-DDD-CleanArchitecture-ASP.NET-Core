namespace CarRentalSystem.Infrastructure.Persistence.Configurations
{
    using Domain.Models;
    using Domain.Models.CarAds;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CarAdConfiguration : IEntityTypeConfiguration<CarAd>
    {
        public void Configure(EntityTypeBuilder<CarAd> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasOne(c => c.Manufacturer)
                .WithMany()
                .HasForeignKey("ManufacturerId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder
                .Property(c => c.IsAvailable)
                .IsRequired();

            builder
                .Property(c => c.Image)
                .IsRequired()
                .HasMaxLength(ModelConstants.Common.MAX_URL_LENGTH);

            builder
                .Property(c => c.Model)
                .IsRequired();

            builder
                .OwnsOne(
                    c => c.Options,
                    o =>
                    {
                        o.WithOwner();
                        o.Property(pn => pn.NumberOfSeats);
                        o.Property(pn => pn.HasClimateControl);

                        o.OwnsOne(
                            op => op.TransmissionType,
                            t =>
                            {
                                t.WithOwner();
                                t.Property(tr => tr.Value);
                            });
                    });
        }
    }
}
