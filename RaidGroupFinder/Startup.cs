using BlazorSignalRApp.Server.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaidGroupFinder.Areas.Identity;
using RaidGroupFinder.Data;
using RaidGroupFinder.Hubs;
using System;
using System.Linq;
using TrainerCodeHubGroupFinder.Hubs;

namespace RaidGroupFinder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")), ServiceLifetime.Transient);

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddTransient<DbService>();
            services.AddSingleton<FixedSizedQueueService>();

            services.AddControllersWithViews();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = Environment.GetEnvironmentVariable("AGClientId");
                options.ClientSecret = Environment.GetEnvironmentVariable("AGClientSecret");

                //// Must have this to populate the tokens ...
                //options.SaveTokens = true;

                //// When mapped, Google properties become claims. For example ...
                //options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseAuthentication();

            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<RaidHub>("/raidhub");
                endpoints.MapHub<TrainerCodeHub>("/trainercodehub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
