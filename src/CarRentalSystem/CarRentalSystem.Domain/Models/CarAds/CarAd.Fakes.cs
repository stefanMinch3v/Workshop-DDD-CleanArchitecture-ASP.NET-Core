namespace CarRentalSystem.Domain.Models.CarAds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using FakeItEasy;

    public class CarAdFakes
    {
        public class CarAdDummyFactory : IDummyFactory
        {
            public bool CanCreate(Type type) => type == typeof(CarAd);

            public object? Create(Type type) => Data.GetCarAd();

            public Priority Priority => Priority.Default;
        }

        public static class Data
        {
            public static IEnumerable<CarAd> GetCarAds(int count = 10)
                => Enumerable
                    .Range(1, count)
                    .Select(i => GetCarAd(i))
                    .Concat(Enumerable
                        .Range(count + 1, count * 2)
                        .Select(i => GetCarAd(i, false)))
                    .ToList();

            public static CarAd GetCarAd(int id = 1, bool isAvailable = true)
                => new Faker<CarAd>()
                    .CustomInstantiator(f => new CarAd(
                        new Manufacturer($"Manufacturer{id}"),
                        A.Dummy<Category>(), 
                        A.Dummy<Options>(),
                        f.Random.Number(100, 200),
                        f.Image.PicsumUrl(),
                        f.Lorem.Letter(10),
                        isAvailable))
                    .Generate();
        }
    }
}
