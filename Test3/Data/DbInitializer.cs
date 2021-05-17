using Petfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PetfinderContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            //var users = new User[]
            //{
            //new User{UserName = "Test 1",Email= "test@test.com"}
            //};
            //foreach (User u in users)
            //{
            //    context.Users.Add(u);
            //}
            //context.SaveChanges();

            //var shelters = new Shelter[]
            //{
            //new Shelter{Name = "Shelter 1", User = users.First()}
            //};
            //foreach (Shelter s in shelters)
            //{
            //    context.Shelters.Add(s);
            //}
            //context.SaveChanges();

            //var pets = new Pet[]
            //{
            //new Pet{Name = "Pimpek", Shelter = shelters.First()},
            //new Pet{Name = "Dupek", Shelter = shelters.First()}
            //};
            //foreach (Pet p in pets)
            //{
            //    context.Pets.Add(p);
            //}
            //context.SaveChanges();

            //foreach (Pet p in pets)
            //{
            //    shelters.FirstOrDefault().Pets.Add(p);
            //}
        }
    }
}

