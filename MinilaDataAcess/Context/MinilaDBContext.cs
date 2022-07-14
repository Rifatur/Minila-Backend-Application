using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinilaDataAcess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinilaDataAcess.Context
{
    public class MinilaDBContext : IdentityDbContext
    {
        public MinilaDBContext(DbContextOptions option): base(option)
        {

        }
        public  DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students  { get; set; }
        public DbSet<Chauffeur> Chauffeurs { get; set; }
        public DbSet<TripRequest> TripRequest { get; set; }
    }
}
