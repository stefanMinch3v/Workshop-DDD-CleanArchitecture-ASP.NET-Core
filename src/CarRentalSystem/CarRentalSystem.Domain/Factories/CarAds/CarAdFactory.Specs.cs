namespace CarRentalSystem.Domain.Factories.CarAds
{
    using Exceptions;
    using FluentAssertions;
    using Models.CarAds;
    using System;
    using Xunit;

    public class CarAdFactorySpecs
    {
        [Fact]
        public void CreateAdShouldThrowIfCategoryIsNotSet()
        {
            // Arrange
            var carAdFactory = new CarAdFactory();

            // Act
            Action act = () => carAdFactory
                .WithManufacturer("test")
                .WithOptions(true, 4, TransmissionType.Manual)
                .Build();

            // Assert
            act.Should().Throw<InvalidCarAdException>();
        }

        [Fact]
        public void CreateAdShouldThrowIfManufacturerIsNotSet()
        {
            // Arrange
            var carAdFactory = new CarAdFactory();

            // Act
            Action act = () => carAdFactory
                .WithCategory("Economy", "test descr")
                .WithOptions(true, 3, TransmissionType.Automatic)
                .Build();

            // Assert
            act.Should().Throw<InvalidCarAdException>();
        }

        [Fact]
        public void CreateAdShouldThrowIfOptionIsNotSet()
        {
            // Arrange
            var carAdFactory = new CarAdFactory();

            // Act
            Action act = () => carAdFactory
                .WithCategory("Economy", "test descr")
                .WithManufacturer("test")
                .Build();

            // Assert
            act.Should().Throw<InvalidCarAdException>();
        }

        [Fact]
        public void CreateAdShouldPassIfAllPropertiesAreSet()
        {
            // Arrange
            var carAdFactory = new CarAdFactory();

            // Act
            var carAd = carAdFactory
                .WithCategory("Economy", "test descr")
                .WithOptions(true, 2, TransmissionType.Automatic)
                .WithManufacturer("test")
                .WithImageUrl("test.com")
                .WithModel("model")
                .WithPricePerDay(2.10m)
                .Build();

            // Assert
            carAd.Should().NotBeNull();
        }
    }
}
