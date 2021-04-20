using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITapp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PetfinderContext())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var admin = new Admin { Name = name };
                db.Admins.Add(admin);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.Admins
                            orderby b.Name
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }


        }


    }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual List<Ratings> Ratings { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
    public class Vet
    {
        public int VetId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual List<Ratings> Ratings { get; set; }
    }
    public class Shelter
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Localization { get; set; }
        public virtual List<Pet> Pets { get; set; }
    }
    public class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Vet> Vets { get; set; }
        public virtual List<Shelter> Shelters { get; set; }
        public virtual List<Ratings> Ratings { get; set; }
        public virtual List<Pet> Pets { get; set; }

    }
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public virtual User User { get; set; }
    }
    public class Ratings
    {
        [Key]
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public string Rating { get; set; }
        public virtual Vet Vet { get; set; }
        public virtual User User { get; set; }
    }
    public class Pet
    {
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int Origins { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public int Difficulty { get; set; }
        public int ShelterId { get; set; }
        public string PhotoUrl { get; set; }
        public virtual Shelter Shelter { get; set; }
    }
    public class PetfinderContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Pet> Pets { get; set; }

    }


}


