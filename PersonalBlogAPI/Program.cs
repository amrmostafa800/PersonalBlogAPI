
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PersonalBlogAPI.Repositories;

namespace PersonalBlogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

#if DEBUG
            var filePath = $"bin\\Debug\\net8.0\\Photos";
#else
        var filePath = "Photos";
#endif

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            // Add services to the container.

            builder.Services.AddScoped<BlogRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), filePath)),
                RequestPath = "/Photos"
            });
            ////Enable directory browsing
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                Path.Combine(Directory.GetCurrentDirectory(), filePath)),
            //    RequestPath = "/Photos"
            //});

            app.MapControllers();

            app.Run();
        }
    }
}
