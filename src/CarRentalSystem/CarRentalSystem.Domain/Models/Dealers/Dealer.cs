namespace CarRentalSystem.Domain.Models.Dealers
{
    using CarAds;
    using Common;
    using Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    using static ModelConstants;

    public class Dealer : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<CarAd> carAds;

        private Dealer(string name)
        {
            this.Name = name;
            this.carAds = new HashSet<CarAd>();

            this.PhoneNumber = null!;
        }

        internal Dealer(string name, PhoneNumber phoneNumber)
        {
            Guard.ForStringLength<InvalidDealerException>(
                name, 
                Dealers.MIN_LENGTH, 
                Dealers.MAX_LENGTH, 
                nameof(name));

            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.carAds = new HashSet<CarAd>();
        }

        public string Name { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<CarAd> CarAds => this.carAds.ToList();

        public void AddCarAd(CarAd carAd) => this.carAds.Add(carAd);
    }
}
