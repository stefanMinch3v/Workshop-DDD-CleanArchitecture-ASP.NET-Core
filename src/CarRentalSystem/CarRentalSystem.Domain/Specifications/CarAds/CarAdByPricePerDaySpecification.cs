﻿namespace CarRentalSystem.Domain.Specifications.CarAds
{
    using Models.CarAds;
    using System;
    using System.Linq.Expressions;

    public class CarAdByPricePerDaySpecification : Specification<CarAd>
    {
        private readonly decimal minPrice;
        private readonly decimal maxPrice;

        public CarAdByPricePerDaySpecification(
            decimal? minPrice = default, 
            decimal? maxPrice = decimal.MaxValue)
        {
            this.minPrice = minPrice ?? default;
            this.maxPrice = maxPrice ?? decimal.MaxValue;
        }

        public override Expression<Func<CarAd, bool>> ToExpression()
            => carAd => this.minPrice < carAd.Price && carAd.Price < this.maxPrice;
    }
}
