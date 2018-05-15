using Microsoft.EntityFrameworkCore;
 
namespace Planner.Models
{
    public class PlannerContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<GuestList> GuestList { get; set; }
    }
}