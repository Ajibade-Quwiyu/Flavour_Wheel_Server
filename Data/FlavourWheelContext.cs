using Microsoft.EntityFrameworkCore;
using Flavour_Wheel_Server.Model;

namespace Flavour_Wheel_Server.Data
{
    public class FlavourWheelContext : DbContext
    {
        public FlavourWheelContext(DbContextOptions<FlavourWheelContext> options)
            : base(options)
        {
        }

        public DbSet<FlavourWheel> FlavourWheels { get; set; }
        public DbSet<AdminServer> AdminServers { get; set; }
    }
}
