namespace CarRentalSystem.Domain.Models.CarAds
{
    using Domain.Factories.CarAds;
    using FluentAssertions;
    using Xunit;

    public class CarAdSpecs
    {
        [Fact]
        public void ChangeAvailabilityShouldMutateIsAvailable()
        {
            // Arrange
            var carAdFactory = new CarAdFactory();
            var carAd = carAdFactory
                .WithCategory("Economy", "test descr")
                .WithOptions(true, 2, TransmissionType.Automatic)
                .WithManufacturer("test")
                .WithImageUrl("test.com")
                .WithModel("model")
                .WithPricePerDay(2.10m)
                .Build();

            // Act
            carAd.ChangeAvailability();

            // Assert
            carAd.IsAvailable.Should().BeFalse();
        }
    }
}
