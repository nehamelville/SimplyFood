using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplyFood.Areas.Identity.Data;
using SimplyFood.Data;

[assembly: HostingStartup(typeof(SimplyFood.Areas.Identity.IdentityHostingStartup))]
namespace SimplyFood.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SimplyFoodDBContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("SimplyFoodDBContextConnection")));

                services.AddDefaultIdentity<SimplyFoodUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SimplyFoodDBContext>();
            });
        }
    }
}