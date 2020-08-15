namespace CarRentalSystem.Application.Features.CarAds.Commands.Create
{
    using Application.Contracts;
    using Application.Features.Dealers;
    using CarRentalSystem.Domain.Common;
    using CarRentalSystem.Domain.Models.CarAds;
    using Domain.Factories.CarAds;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateCarAdCommand : IRequest<CreateCarAdOutputModel>
    {
        public CreateCarAdCommand(
            string manufacturer,
            string model,
            int category,
            string imageUrl,
            decimal pricePerDay,
            bool climateControl,
            int numberOfSeats,
            int transmissionType)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.NumberOfSeats = numberOfSeats;
            this.PricePerDay = pricePerDay;
            this.TransmissionType = transmissionType;
            this.Category = category;
            this.ClimateControl = climateControl;
            this.ImageUrl = imageUrl;
        }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Category { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerDay { get; set; }
        public bool ClimateControl { get; set; }
        public int NumberOfSeats { get; set; }
        public int TransmissionType { get; set; }

        public class CreateCarAdCommandHandler : IRequestHandler<CreateCarAdCommand, CreateCarAdOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IDealerRepository dealerRepository;
            private readonly ICarAdRepository carAdRepository;
            private readonly ICarAdFactory carAdFactory;

            public CreateCarAdCommandHandler(
                ICurrentUser currentUser,
                IDealerRepository dealerRepository,
                ICarAdRepository carAdRepository,
                ICarAdFactory carAdFactory)
            {
                this.currentUser = currentUser;
                this.dealerRepository = dealerRepository;
                this.carAdRepository = carAdRepository;
                this.carAdFactory = carAdFactory;
            }

            public async Task<CreateCarAdOutputModel> Handle(CreateCarAdCommand request, CancellationToken cancellationToken)
            {
                var userId = this.currentUser.UserId;
                var dealer = await this.dealerRepository.FindByUser(userId, cancellationToken);

                var category = await this.carAdRepository.GetCategory(request.Category, cancellationToken);

                var manufacturer = await this.carAdRepository.GetManufacturer(request.Manufacturer, cancellationToken);

                var factory = manufacturer == null
                    ? this.carAdFactory.WithManufacturer(request.Manufacturer)
                    : this.carAdFactory.WithManufacturer(manufacturer);

                var carAd = factory
                    .WithModel(request.Model)
                    .WithCategory(category)
                    .WithImageUrl(request.ImageUrl)
                    .WithPricePerDay(request.PricePerDay)
                    .WithOptions(
                        request.ClimateControl,
                        request.NumberOfSeats,
                        Enumeration.FromValue<TransmissionType>(request.TransmissionType))
                    .Build();

                dealer.AddCarAd(carAd);

                await this.carAdRepository.Save(carAd, cancellationToken);

                return new CreateCarAdOutputModel(carAd.Id);
            }
        }
    }
}
