namespace CarRentalSystem.Application.Features.CarAds
{
    using Contracts;
    using Domain.Models.CarAds;
    using Domain.Specifications;
    using Queries.Search;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICarAdRepository : IRepository<CarAd>
    {
        Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
            Specification<CarAd> specification,
            CancellationToken cancellationToken = default);

        Task<int> Total(CancellationToken cancellationToken = default);

        Task<Category> GetCategory(int category, CancellationToken cancellationToken);

        Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken);
    }
}
