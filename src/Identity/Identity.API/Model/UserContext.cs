using Identity.Api.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Model
{
    public class UserContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserMap(modelBuilder.Entity<User>());
        }
    }
}
