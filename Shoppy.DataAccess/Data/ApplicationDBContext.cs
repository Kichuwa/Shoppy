using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shoppy.Models;

namespace Shoppy.DataAccess.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        //Data layer for Database communication
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
    }
}
