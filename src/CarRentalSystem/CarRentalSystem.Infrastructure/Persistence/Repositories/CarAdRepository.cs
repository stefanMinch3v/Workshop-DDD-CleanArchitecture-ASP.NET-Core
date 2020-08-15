namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using Application.Features.CarAds;
    using Application.Features.CarAds.Queries.Search;
    using AutoMapper;
    using Domain.Models.CarAds;
    using Domain.Specifications;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CarAdRepository : DataRepository<CarAd>, ICarAdRepository
    {
        private readonly IMapper mapper;

        public CarAdRepository(CarRentalDbContext db, IMapper mapper)
            : base(db) 
            => this.mapper = mapper;

        public async Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
            Specification<CarAd> specification,
            CancellationToken cancellationToken = default)
            //var query = this.AllAvailable();
            //if (!string.IsNullOrWhiteSpace(manufacturer))
            //{
            //    query = query
            //        .Where(car => EF
            //            .Functions
            //            .Like(car.Manufacturer.Name, $"%{manufacturer}%"));
            //}
            => await this.mapper
                .ProjectTo<CarAdListingModel>(this
                    .AllAvailable()
                    .Where(specification))
                .ToListAsync(cancellationToken);

        public async Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken)
            => await this
                .Data
                .Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        public async Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken)
            => await this
                .Data
                .Manufacturers
                .FirstOrDefaultAsync(c => c.Name == manufacturer, cancellationToken);

        public async Task<int> Total(CancellationToken cancellationToken = default)
            => await this
                .AllAvailable()
                .CountAsync(cancellationToken);

        private IQueryable<CarAd> AllAvailable()
            => this
                .All()
                .Where(car => car.IsAvailable);
    }
}
