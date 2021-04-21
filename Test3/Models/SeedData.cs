using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petfinder.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PetfinderContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PetfinderContext>>()))
            {
                // Look for any movies.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(
                    new User
                    {
                        Name = "Test 1",
                        PhoneNumber = "Phone test 1"
                    },

                    new User
                    {
                        Name = "Test 2",
                        PhoneNumber = "Phone test 2"
                    },

                    new User
                    {
                        Name = "Test 2",
                        PhoneNumber = "Phone test 2"
                    },

                    new User
                    {
                        Name = "Test 2",
                        PhoneNumber = "Phone test 2"
                    },

                    new User
                    {
                        Name = "Test 2",
                        PhoneNumber = "Phone test 2"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
