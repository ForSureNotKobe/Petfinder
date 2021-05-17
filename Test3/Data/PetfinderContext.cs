using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class PetfinderContext : IdentityDbContext<User>
    {
        public PetfinderContext(DbContextOptions<PetfinderContext> options) : base(options)
        { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rating> Admins { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Pet> Pets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Shelter)
                .WithMany(s => s.Pets);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings);
        }
    }
}
