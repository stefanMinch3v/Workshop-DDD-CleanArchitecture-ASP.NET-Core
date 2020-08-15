namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using Application.Features.Dealers;
    using Domain.Exceptions;
    using Domain.Models.Dealers;
    using Infrastructure.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DealerRepository : DataRepository<Dealer>, IDealerRepository
    {
        public DealerRepository(CarRentalDbContext context)
            : base(context)
        {
        }

        public Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken)
            => this.FindByUser(userId, user => user.Dealer!, cancellationToken);

        private async Task<T> FindByUser<T>(
            string userId,
            Expression<Func<User, T>> selector,
            CancellationToken cancellationToken = default)
        {
            var dealerData = await this
                .Data
                .Users
                .Where(u => u.Id == userId)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);

            if (dealerData == null)
            {
                throw new InvalidDealerException("This user is not a dealer.");
            }

            return dealerData;
        }
    }
}
