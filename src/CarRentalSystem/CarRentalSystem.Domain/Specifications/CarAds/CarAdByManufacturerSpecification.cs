namespace CarRentalSystem.Domain.Specifications.CarAds
{
    using Domain.Models.CarAds;
    using System;
    using System.Linq.Expressions;

    public class CarAdByManufacturerSpecification : Specification<CarAd>
    {
        private readonly string? manufacturer;

        public CarAdByManufacturerSpecification(string? manufacturer)
            => this.manufacturer = manufacturer;

        public override Expression<Func<CarAd, bool>> ToExpression()
            => carAd => carAd.Manufacturer.Name.ToLower()
                .Contains(this.manufacturer!.ToLower());

        protected override bool Include => this.manufacturer != null;
    }
}
