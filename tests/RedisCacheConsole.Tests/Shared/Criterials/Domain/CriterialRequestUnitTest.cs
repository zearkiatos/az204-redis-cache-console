using Bogus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedisCacheConsole.Shared.Criterials.Domain;
namespace RedisCacheConsole.Tests.Shared.Criterials.Domain
{
    public class CriterialRequestUnitTest
    {
        private static readonly Faker faker = new();

        [Fact]
        public void Given_A_Limit_When_Initialized_Then_LimitIsSetCorrectly()
        {
            var expectedLimit = faker.Random.Int(1, 100);
            var criterial = new CriterialRequest();

            criterial.Limit = expectedLimit;

            Assert.Equal(expectedLimit, criterial.Limit);
        }

        [Fact]
        public void Given_A_Offset_When_Initialized_Then_OffsetIsSetCorrectly()
        {
            var expectedOffset = faker.Random.Int(0, 100);
            var criterial = new CriterialRequest();

            criterial.Offset = expectedOffset;

            Assert.Equal(expectedOffset, criterial.Offset);
        }

        [Fact]
        public void Given_A_Order_When_Initialized_Then_OrderIsSetCorrectly()
        {
            var expectedOrder = faker.Random.Word();
            var criterial = new CriterialRequest();

            criterial.Order = expectedOrder;

            Assert.Equal(expectedOrder, criterial.Order);
        }

        [Fact]
        public void Given_A_Filter_When_Initialized_Then_FilterIsSetCorrectly()
        {
            var filters = new List<FilterRequest>();
            var expectedFilter = faker.Random.Word();
            filters.Add(new FilterRequest { Value = expectedFilter });
            var criterial = new CriterialRequest();

            criterial.Filters = filters;

            Assert.Equal(expectedFilter, criterial.Filters[0].Value);
        }

        [Fact]
        public void Given_A_CriterialRequest_When_Serialized_Then_UsesJsonPropertyNames()
        {
            var request = new CriterialRequest
            {
                Limit = 10,
                Order = "Name ASC",
                Offset = 2,
                Filters = new List<FilterRequest>
        {
            new() { Field = "Name", Operator = "=", Value = "Bike" }
        }
            };

            var json = JsonConvert.SerializeObject(request);
            var result = JObject.Parse(json);

            Assert.Equal(10, (int)result["limit"]!);
            Assert.Equal("Name ASC", (string)result["order"]!);
            Assert.Equal(2, (int)result["offset"]!);
            Assert.Equal("Name", (string)result["filters"]![0]!["field"]!);
            Assert.Equal("=", (string)result["filters"]![0]!["operator"]!);
            Assert.Equal("Bike", (string)result["filters"]![0]!["value"]!);
        }

        [Fact]
        public void Given_A_JsonString_When_Deserialized_Then_MapsToCriterialRequest()
        {
            const string json = """
        {
          "limit": 10,
          "order": "Name ASC",
          "offset": 2,
          "filters": [
            { "field": "Name", "operator": "=", "value": "Bike" }
          ]
        }
        """;

            var result = JsonConvert.DeserializeObject<CriterialRequest>(json);

            Assert.NotNull(result);
            Assert.Equal(10, result.Limit);
            Assert.Equal("Name ASC", result.Order);
            Assert.Equal(2, result.Offset);
            Assert.Equal("Name", result.Filters[0].Field);
            Assert.Equal("=", result.Filters[0].Operator);
            Assert.Equal("Bike", result.Filters[0].Value);
        }
    }
}