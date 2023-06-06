using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShoppyApp.Model;

namespace ShoppyApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        //Data layer for Database communication
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Category { get; set; }
    }
}
