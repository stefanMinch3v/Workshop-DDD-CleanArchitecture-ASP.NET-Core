namespace CarRentalSystem.Domain.Common
{
    using CarRentalSystem.Domain.Models.CarAds;
    using FluentAssertions;
    using Xunit;

    public class EntitySpecs
    {
        [Fact]
        public void EntitiesWithEqualIdsShouldBeEqual()
        {
            // Arrange
            var first = new Manufacturer("First").SetId(1);
            var second = new Manufacturer("Second").SetId(1);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }
    }

    internal static class EntityExtensions
    {
        public static Entity<T> SetId<T>(this Entity<T> entity, int id)
            where T : struct
        {
            entity
                .GetType()
                .BaseType!
                .GetProperty(nameof(Entity<T>.Id))!
                .GetSetMethod(true)!
                .Invoke(entity, new object[] { id });

            return entity;
        }
    }
}
