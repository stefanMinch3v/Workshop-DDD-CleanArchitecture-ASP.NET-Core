namespace CarRentalSystem.Domain.Models.CarAds
{
    using Exceptions;
    using Common;

    public class Category : Entity<int>
    {
        internal Category(string name, string description)
        {
            Guard.AgainstEmptyString<InvalidCarAdException>(name, nameof(name));
            Guard.AgainstEmptyString<InvalidCarAdException>(description, nameof(description));

            this.Name = name;
            this.Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
