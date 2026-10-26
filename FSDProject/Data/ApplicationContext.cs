using FSDProject.Cascade;
using FSDProject.Models;
using FSDProject.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSDProject.Data
{
    public class ApplicationContext:DbContext 
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
