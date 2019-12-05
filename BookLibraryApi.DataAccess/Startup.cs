using BookLibraryApi.DataAccess.EF;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using BookLibraryApi.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookLibraryApi.DataAccess
{
    public class Startup
    {
        public static void OnInit(IServiceCollection services,IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                b=>b.MigrationsAssembly("BookLibraryApi")));

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IRepository<Book>, BookRepository>();
            services.AddTransient<IRepository<Reader>, ReaderRepository>();
        }
    }
}
