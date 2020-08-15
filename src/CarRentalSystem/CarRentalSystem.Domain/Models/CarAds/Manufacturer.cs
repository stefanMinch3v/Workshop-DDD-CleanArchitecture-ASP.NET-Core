namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;

    public class Manufacturer : Entity<int>
    {
        internal Manufacturer(string name)
        {
            Guard.AgainstEmptyString<InvalidCarAdException>(name, nameof(name));

            this.Name = name;
        }

        public string Name { get; }
    }
}
