namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;
    using System.Collections.Generic;
    using System.Linq;
    using static ModelConstants;

    public class CarAd : Entity<int>, IAggregateRoot
    {
        private static readonly IEnumerable<Category> AllowedCategories
            = new CategoryData().GetData().Cast<Category>();

        private CarAd(
            decimal price,
            string image,
            string model,
            bool isAvailable)
        {
            this.Price = price;
            this.Image = image;
            this.Model = model;
            this.IsAvailable = isAvailable;

            this.Manufacturer = default!;
            this.Category = default!;
            this.Options = default!;
        }

        internal CarAd(
            Manufacturer manufacturer, 
            Category category, 
            Options options,
            decimal price,
            string image,
            string model,
            bool isAvailable)
        {
            this.ValidateInput(price, image, model);
            //this.ValidateCategory(category);

            this.Manufacturer = manufacturer;
            this.Category = category;
            this.Options = options;
            this.Price = price;
            this.Image = image;
            this.Model = model;
            this.IsAvailable = isAvailable;
        }

        public decimal Price { get; }
        public string Model { get; }
        public string Image { get; }
        public Manufacturer Manufacturer { get; }
        public Category Category { get; }
        public Options Options { get; }

        public bool IsAvailable { get; private set; }

        public void ChangeAvailability() => this.IsAvailable = !this.IsAvailable;

        private void ValidateInput(decimal price, string image, string model)
        {
            Guard.AgainstOutOfRange<InvalidCarAdException>(
                price,
                CarAds.MIN_LENGTH_PRICE,
                CarAds.MAX_LENGTH_PRICE,
                nameof(price));

            Guard.ForValidUrl<InvalidCarAdException>(image, nameof(image));

            Guard.AgainstEmptyString<InvalidCarAdException>(model, nameof(model));
        }

        private void ValidateCategory(Category category)
        {
            var categoryName = category.Name;

            if (AllowedCategories.Any(c => c.Name == categoryName))
            {
                return;
            }

            var allowedCategoryNames = string.Join(", ", AllowedCategories.Select(c => $"'{c.Name}'"));

            throw new InvalidCarAdException(
                $"'{categoryName}' is not a valid category. Allowed values are: {allowedCategoryNames}.");
        }
    }
}
