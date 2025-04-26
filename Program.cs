using Healio.Models;
using Healio.Models.MappingProfiles;
using Healio.Services;
using Microsoft.EntityFrameworkCore;

namespace Healio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<HealioDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

            builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options => options.LoginPath = "/Login");


            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();
            // Configure the HTTP request pipeline.


            if (app.Environment.IsDevelopment())
            {
                Console.WriteLine("asd");
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
