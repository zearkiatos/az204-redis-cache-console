using RedisCacheConsole.Shared.Criterials.Domain;

namespace RedisCacheConsole.Tests.Shared.Criterials.Domain
{
    public class FilterTests
    {
        [Fact]
        public void Given_A_Filter_Constructor_When_InitializedWithValues_Then_PropertiesAreSet()
        {
            var filter = new Filter("Name", "=", "Chain");

            Assert.Equal("Name", filter.FilterField);
            Assert.Equal("=", filter.FilterOperator);
            Assert.Equal("Chain", filter.FilterValue);
        }

        [Fact]
        public void Given_A_Filter_When_SerializationCalled_Then_ReturnsFieldOperatorValueSeparatedBySpaces()
        {
            var filter = new Filter("ListPrice", ">", "100");

            var result = filter.Serialization();

            Assert.Equal("ListPrice > 100", result);
        }

        [Fact]
        public void Given_A_Filter_ParameterlessConstructor_When_PropertiesSetIndividually_Then_SerializationReflectsValues()
        {
            var filter = new Filter
            {
                FilterField = "Name",
                FilterOperator = "LIKE",
                FilterValue = "%bike%"
            };

            Assert.Equal("Name LIKE %bike%", filter.Serialization());
        }
    }
}
