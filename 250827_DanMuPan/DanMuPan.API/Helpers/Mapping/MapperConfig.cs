using AutoMapper;

namespace DanMuPan.API.Helpers.Mapping;

public class MapperConfig
{
    public MapperConfig(ILoggerFactory loggerFactory)
    {
        var mapperConfig = new MapperConfiguration(
            config =>
            {
                config.AddProfile<UserProfile>();
            },
            loggerFactory
        );
    }
}
