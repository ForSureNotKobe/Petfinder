using Microsoft.AspNetCore.Identity;
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

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                    new User { UserName = "Jan Kowalski", Email = "J.Kowalski@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Adam Wojcik", Email = "A.Wojcik@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Grzegorz Malinowski", Email = "G.Malinowski@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Anna Sklodowska", Email = "A.Sklodowska@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Adrian Cur", Email = "A.Cur@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Natalia Biernacka", Email = "N.Biernacka@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Jakub Wierzbicki", Email = "J.Wierzbicki@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Kamila Smiech", Email = "K.Smiech@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Karol Godlewski", Email = "K.Godlewski@gamil.com", PhoneNumber = "123456789" },
                    new User { UserName = "Marcelina Zak", Email = "M.Zak@gamil.com", PhoneNumber = "123456789" }
            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            foreach (User u in users)
            {
                passwordHasher.HashPassword(u, "Admin123@");
                context.Users.Add(u);
            }
            context.SaveChanges();


            var shelters = new Shelter[]
            {
                    new Shelter { Name = "PetHouse", Email = "Pet.House@gamil.com", Nip = 123, PhoneNumber = "123456789", Address = "Wroclaw, Olbin" },
                    new Shelter { Name = "WroPets", Email = "Wro.Pets@gamil.com", Nip = 456, PhoneNumber = "123456789", Address = "Wroclaw, plac grunwaldzki" }
            };

            foreach (Shelter s in shelters)
            {
                context.Shelters.Add(s);
            }
            context.SaveChanges();

            var pets = new Pet[]
            {
                    new Pet { ShelterId = 1, Name = "Azor", Age = 2, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)0, Description = "Owczarek niemiecki, dlugo-wlosy, opalany", Size = (Size?)2, Difficulty = (Difficulty?)2, PhotoUrl = null },
                    new Pet { ShelterId = 2, Name = "Czesio", Age = 1, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)2, Description = "Chomik, jasna siersc, mlody", Size = (Size?)0, Difficulty = 0, PhotoUrl = null  },
                    new Pet { ShelterId = 2, Name = "Oskar", Age = 5, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)0, Description = "York, bialy", Size = (Size?)0, Difficulty = (Difficulty?)1 , PhotoUrl = null },
                    new Pet { ShelterId = 2, Name = "Ella", Age = 3, Sex = (Sex?)0, Origins = 0, BreedType = (BreedType?)1, Description = "Kot, Rudy", Size = (Size?)1, Difficulty = 0 , PhotoUrl = null },
                    new Pet { ShelterId = 2, Name = "Bobek", Age = 4, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)2, Description = "Maly zolwik", Size = (Size?)0, Difficulty = 0, PhotoUrl = null  },
                    new Pet { ShelterId = 2, Name = "Aria", Age = 1, Sex = (Sex?)0, Origins = 0, BreedType = (BreedType?)1, Description = "Kot perski, zloty", Size = (Size?)0, Difficulty = (Difficulty?)2 , PhotoUrl = null },
                    new Pet { ShelterId = 2, Name = "Kiel", Age = 8, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)0, Description = "Golden red river, ciemny wlos", Size = (Size?)2, Difficulty = (Difficulty?)1, PhotoUrl = null  },
                    new Pet { ShelterId = 2, Name = "Cymek", Age = 6, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)1, Description = "Kot, czarna karnacja", Size = 0, Difficulty = (Difficulty?)1, PhotoUrl = null  },
                    new Pet { ShelterId = 1, Name = "Mati", Age = 3, Sex = (Sex?)0, Origins = 0, BreedType = (BreedType?)0, Description = "Jamnik, ciemna siersc", Size = 0, Difficulty = (Difficulty?)1, PhotoUrl = null  },
                    new Pet { ShelterId = 1, Name = "Alex", Age = 1, Sex = (Sex?)0, Origins = 0, BreedType = (BreedType?)2, Description = "Chomik, brazowy", Size = 0, Difficulty = 0, PhotoUrl = null  },
                    new Pet { ShelterId = 1, Name = "Oli", Age = 0, Sex = (Sex?)0, Origins = 0, BreedType = (BreedType?)2, Description = "Mysz domowa, szara", Size = 0, Difficulty = 0 , PhotoUrl = null },
                    new Pet { ShelterId = 1, Name = "Benia", Age = 1, Sex = (Sex?)0, Origins = 0, BreedType = (BreedType?)2, Description = "Wiewiorka czarna", Size = 0, Difficulty = (Difficulty?)2 , PhotoUrl = null },
                    new Pet { ShelterId = 1, Name = "Adek", Age = 4, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)0, Description = "Owczarek niemiecki, brazowy", Size = (Size?)2, Difficulty = (Difficulty?)2, PhotoUrl = null  },
                    new Pet { ShelterId = 1, Name = "Zosia", Age = 2, Sex = (Sex?)0, Origins = 0, BreedType = (BreedType?)1, Description = "Minecoon, jasny wlos", Size = (Size?)1, Difficulty = (Difficulty?)2 , PhotoUrl = null },
                    new Pet { ShelterId = 1, Name = "Edek", Age = 3, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)1, Description = "Kot Syberyjski, jasny", Size = 0, Difficulty = (Difficulty?)1, PhotoUrl = null  },
                    new Pet { ShelterId = 1, Name = "Enio", Age = 1, Sex = (Sex?)1, Origins = 0, BreedType = (BreedType?)2, Description = "Chomik, szary", Size = 0, Difficulty = 0, PhotoUrl = null  }
            };

            foreach (Pet p in pets)
            {
                context.Pets.Add(p);
            }

            context.SaveChanges();
        }
    }
}

