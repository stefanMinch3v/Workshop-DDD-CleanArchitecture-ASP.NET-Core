namespace CarRentalSystem.Domain.Factories.CarAds
{
    using CarRentalSystem.Domain.Exceptions;
    using Models.CarAds;

    internal class CarAdFactory : ICarAdFactory
    {
        private Category category = default!;
        private Manufacturer manufacturer = default!;
        private Options options = default!;
        private string imageUrl = default!;
        private string model = default!;
        private decimal pricePerDay = default!;

        private bool isCategorySet = false;
        private bool isManufacturerSet = false;
        private bool isOptionSet = false;

        public CarAd Build()
        {
            if (!this.isCategorySet || !this.isManufacturerSet || !this.isOptionSet)
            {
                throw new InvalidCarAdException($"{nameof(Category)}, {nameof(Manufacturer)} and {nameof(Options)} are required!");
            }

            return new CarAd(
                this.manufacturer, 
                this.category, 
                this.options, 
                this.pricePerDay, 
                this.imageUrl, 
                this.model,
                true);
        }

        public ICarAdFactory WithCategory(string name, string description)
            => this.WithCategory(new Category(name, description));

        public ICarAdFactory WithCategory(Category category)
        {
            this.category = category;
            this.isCategorySet = true;
            return this;
        }

        public ICarAdFactory WithImageUrl(string imageUrl)
        {
            this.imageUrl = imageUrl;
            return this;
        }

        public ICarAdFactory WithManufacturer(string name)
            => this.WithManufacturer(new Manufacturer(name));

        public ICarAdFactory WithManufacturer(Manufacturer manufacturer)
        {
            this.manufacturer = manufacturer;
            this.isManufacturerSet = true;
            return this;
        }

        public ICarAdFactory WithModel(string model)
        {
            this.model = model;
            return this;
        }

        public ICarAdFactory WithOptions(bool hasClimateControl, int numberOfSeats, TransmissionType transmissionType)
            => this.WithOptions(new Options(hasClimateControl, numberOfSeats, transmissionType));

        public ICarAdFactory WithOptions(Options options)
        {
            this.options = options;
            this.isOptionSet = true;
            return this;
        }

        public ICarAdFactory WithPricePerDay(decimal pricePerDay)
        {
            this.pricePerDay = pricePerDay;
            return this;
        }
    }
}
