namespace e_commerce_store_service_host.Server
{
    using Microsoft.EntityFrameworkCore;
    using Models.Data;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connect to the database
            var connectionString = "Server=localhost,1433;Database=csce361;User Id=sa;Password=Placeh0lder!Passw0rd;TrustServerCertificate=True;";
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
