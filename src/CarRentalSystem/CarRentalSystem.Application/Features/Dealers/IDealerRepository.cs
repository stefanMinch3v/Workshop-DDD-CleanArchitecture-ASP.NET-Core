namespace CarRentalSystem.Application.Features.Dealers
{
    using Application.Contracts;
    using Domain.Models.Dealers;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken);
    }
}
