using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RidingApp.DataAccess.Entities;

namespace RidingApp.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<TripRequest> TripRequest { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<RoadWay> RoadWay { get; set; }
        public virtual DbSet<RiderHub> RiderHub { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<PersonalDetails> PersonalDetails { get; set; }
        public virtual DbSet<SystemImage> SystemImage { get; set; }

    }
}
