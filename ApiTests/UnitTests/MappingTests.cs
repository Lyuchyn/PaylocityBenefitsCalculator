using ApiTests.Fixtures;
using AutoMapper;
using Xunit;

namespace ApiTests.UnitTests
{
    public class MappingTests : IClassFixture<AutoMapperFixture>
    {
        private readonly IConfigurationProvider _configuration;

        public MappingTests(AutoMapperFixture autoMapperFixture)
        {
            _configuration = autoMapperFixture.ConfigurationProvider;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}
