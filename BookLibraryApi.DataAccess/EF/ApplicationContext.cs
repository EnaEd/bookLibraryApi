using BookLibraryApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.DataAccess.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
    }
}
