using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petfinder.Data;
using Petfinder.Models;

[assembly: HostingStartup(typeof(Petfinder.Areas.Identity.IdentityHostingStartup))]
namespace Petfinder.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<PetfinderContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("PetfinderContextConnection")));

                //services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<PetfinderContext>();
            });
        }
    }
}