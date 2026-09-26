using System.Collections.Generic;
using RedisCacheConsole.Shared.Criterials.Domain;

namespace RedisCacheConsole.Tests.Shared.Criterials.Domain
{
    public class CriterialTests
    {
        [Fact]
        public void Given_A_Criterial_Constructor_When_InitializedWithNullFilters_Then_FiltersDefaultToEmptyList()
        {
            var criterial = new Criterial(null!, order: null!, offset: 0, limit: 10);

            Assert.False(criterial.HasFilters());
            Assert.Empty(criterial.Filters);
        }

        [Fact]
        public void Given_A_Criterial_WithFilters_When_HasFiltersCalled_Then_ReturnsTrue()
        {
            var filters = new List<Filter> { new Filter("Name", "=", "Bike") };
            var criterial = new Criterial(filters, order: null!, offset: 0, limit: 10);

            Assert.True(criterial.HasFilters());
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("Name ASC", true)]
        public void Given_A_Criterial_When_HasOrderCalled_Then_ReflectsWhetherOrderIsSet(string? order, bool expected)
        {
            var criterial = new Criterial(new List<Filter>(), order!, offset: 0, limit: 10);

            Assert.Equal(expected, criterial.HasOrder());
        }

        [Fact]
        public void Given_A_Criterial_When_PlainFiltersCalled_Then_ReturnsFiltersAsArray()
        {
            var filters = new List<Filter>
            {
                new Filter("Name", "=", "Bike"),
                new Filter("ListPrice", ">", "100")
            };
            var criterial = new Criterial(filters, order: null!, offset: 0, limit: 10);

            var result = criterial.PlainFilters();

            Assert.Equal(filters.ToArray(), result);
        }

        [Fact]
        public void Given_A_Criterial_When_FilterSerializationCalled_Then_JoinsEachFilterWithTrailingComma()
        {
            var filters = new List<Filter>
            {
                new Filter("Name", "=", "Bike"),
                new Filter("ListPrice", ">", "100")
            };
            var criterial = new Criterial(filters, order: null!, offset: 0, limit: 10);

            var result = criterial.FilterSerialization();

            Assert.Equal("Name = Bike,ListPrice > 100,", result);
        }

        [Fact]
        public void Given_A_Criterial_WithFiltersAndOrder_When_SerializeCalled_Then_CombinesAllSegments()
        {
            var filters = new List<Filter> { new Filter("Name", "=", "Bike") };
            var criterial = new Criterial(filters, "Name ASC", offset: 5, limit: 20);

            var result = criterial.Serialize();

            Assert.Equal("Name = Bike,~~Name ASC~~5~~20", result);
        }

        [Fact]
        public void Given_A_Criterial_WithoutFiltersOrOrder_When_SerializeCalled_Then_OnlyIncludesOffsetAndLimit()
        {
            var criterial = new Criterial(new List<Filter>(), order: null!, offset: 0, limit: 10);

            var result = criterial.Serialize();

            Assert.Equal("0~~10", result);
        }
    }
}
