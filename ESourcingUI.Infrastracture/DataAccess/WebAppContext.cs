using ESourcingUI.Core.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ESourcingUI.Infrastracture.DataAccess
{
    public class WebAppContext : IdentityDbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
