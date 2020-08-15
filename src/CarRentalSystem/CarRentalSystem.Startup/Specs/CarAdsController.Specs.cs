namespace CarRentalSystem.Startup.Specs
{
    using Application.Features.CarAds.Queries.Search;
    using Domain.Models.CarAds;
    using FakeItEasy;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Web.Features;
    using Xunit;

    public class CarAdsControllerSpecs
    {
        /// <summary>
        /// Empty lines represent the AAA pattern
        /// </summary>
        /// <param name="totalCarAds"></param>
        [Theory]
        [InlineData(2)]
        public void GetShouldReturnAllCarAdsWithoutQuery(int totalCarAds)
            => MyPipeline
                .Configuration()
                .ShouldMap("/CarAds")

                .To<CarAdsController>(c => c.Search(With.Empty<SearchCarAdsQuery>()))
                .Which(instance => instance
                    .WithData(A.CollectionOfDummy<CarAd>(totalCarAds)))

                .ShouldReturn()
                .ActionResult<SearchCarAdsOutputModel>(result => result
                    .Passing(model => model
                        .CarAds.Count().Should().Be(totalCarAds)));

        [Fact]
        public void GetShouldReturnAvailableCarAdsWithoutQuery()
            => MyPipeline
                .Configuration()
                .ShouldMap("/CarAds")

                .To<CarAdsController>(c => c.Search(With.Empty<SearchCarAdsQuery>()))
                .Which(instance => instance
                    .WithData(CarAdFakes.Data.GetCarAds()))

                .ShouldReturn()
                .ActionResult<SearchCarAdsOutputModel>(result => result
                    .Passing(model => model
                        .CarAds.Count().Should().Be(10)));

        [Theory]
        [InlineData("Manufacturer10")]
        public void SearchShouldReturnFilteredCarAdsWithQuery(string manufacturer)
            => MyPipeline
                .Configuration()
                .ShouldMap($"/CarAds?{nameof(manufacturer)}={manufacturer}")

                .To<CarAdsController>(c => c.Search(new SearchCarAdsQuery { Manufacturer = manufacturer }))
                .Which(instance => instance
                    .WithData(CarAdFakes.Data.GetCarAds()))

                .ShouldReturn()
                .ActionResult<SearchCarAdsOutputModel>(result => result
                    .Passing(model =>
                    {
                        model.CarAds.Count().Should().Be(1);
                        model.CarAds.First().Manufacturer.Should().Be(manufacturer);
                    }));

        [Fact]
        public void SearchShouldHaveCorrectAttributes()
            => MyController<CarAdsController>
                .Calling(c => c.Search(With.Default<SearchCarAdsQuery>()))

                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Get));
    }
}
