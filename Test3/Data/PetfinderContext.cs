using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class PetfinderContext : DbContext
    {
        public PetfinderContext(DbContextOptions<PetfinderContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
}
