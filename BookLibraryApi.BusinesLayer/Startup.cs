using AutoMapper;
using BookLibraryApi.BusinesLayer.MappingProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibraryApi.BusinesLayer
{
    public class Startup
    {
        public static void OnInit(IServiceCollection services, IConfiguration configuration)
        {
            DataAccess.Startup.OnInit(services, configuration);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ReaderMappingProfile());
                cfg.AddProfile(new BookMappingProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
