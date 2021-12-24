using System.Linq;
using ArtHouse.Helpers;
using ArtHouse.Models;
using ArtHouse.Repository.Implementations;
using ArtHouse.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArtHouse
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
            services.AddRazorPages();
            services.AddSession();
            services.AddSingleton<IUserR, UserR>();
            services.AddSingleton<ICommentR, CommentR>();
            services.AddSingleton<ILikerArtR, LikerArtR>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserR userRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.Use(async (context, next) =>
            {
                var a = context.Session.GetString("role");
                var b = context.Session.GetString("exception");
                if (!context.Session.Keys.Contains("role"))
                {
                    context.Session.SetString("role", Roles.Guest.ToString());
                }             
                a = context.Session.GetString("role");
                await next.Invoke();
            });
            app.Use(async (context, next) =>
            {
                if (context.Request.Cookies.ContainsKey("email") && context.Request.Cookies.ContainsKey("password") && context.Session.GetString("role")=="Guest")
                {
                    var email = context.Request.Cookies["email"];
                    var password = context.Request.Cookies["password"];
                    var user = await userRepository.GetUserByEmailAndPasswordAsync(email, password);
                    context.Session.SetString("role", user.Role.ToString());
                    context.Session.Set<User>("Current_user", user);
                }
                await next.Invoke();
            });
                app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
