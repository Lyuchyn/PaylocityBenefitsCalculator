using Api.Mappings;
using AutoMapper;

namespace ApiTests.Fixtures
{
    public class AutoMapperFixture
    {
        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }

        public AutoMapperFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppMappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }
    }
}
