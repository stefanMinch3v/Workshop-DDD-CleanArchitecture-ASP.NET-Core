namespace CarRentalSystem.Infrastructure.Persistence.Configurations
{
    using Domain.Models.CarAds;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ManufactorerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .IsRequired();
        }
    }
}
