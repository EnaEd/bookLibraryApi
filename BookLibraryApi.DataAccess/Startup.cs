using BookLibraryApi.DataAccess.EF;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using BookLibraryApi.DataAccess.Repositories;
using BookLibraryApi.DataAccess.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookLibraryApi.DataAccess
{
    public class Startup
    {
        public static void OnInit(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("BookLibraryApi")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.RequireHttpsMetadata = false;
                      options.TokenValidationParameters = new TokenValidationParameters
                      {

                          ValidateIssuer = true,
                          ValidIssuer = configuration["AuthOptions:ISSUER"],

                          ValidateAudience = true,
                          ValidAudience = configuration["AuthOptions:AUDIENCE"],

                          ValidateLifetime = true,

                          IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AuthOption:Key"])),
                          ValidateIssuerSigningKey = true,
                      };
                  });



            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IRepository<Book>, BookRepository>();
            services.AddTransient<IRepository<Reader>, ReaderRepository>();
            services.AddTransient<ITokenServie, TokenService>();
        }
    }
}
