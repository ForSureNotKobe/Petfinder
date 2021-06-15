using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Petfinder.Areas.Identity.IdentityHostingStartup))]
namespace Petfinder.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //services.AddDbContext<PetfinderContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("PetfinderContextConnection")));

                //services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<PetfinderContext>();
            });
        }
    }
}