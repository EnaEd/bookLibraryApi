using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibraryApi.BusinesLayer
{
    public class Startup
    {
        public static void OnInit(IServiceCollection services, IConfiguration configuration)
        {
            DataAccess.Startup.OnInit(services, configuration);
        }
    }
}
