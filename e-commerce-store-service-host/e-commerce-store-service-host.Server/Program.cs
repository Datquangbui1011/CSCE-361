using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;


namespace e_commerce_store_service_host.Server
{
    using e_commerce_store_service_host.Server.Accessors;

    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connect to the database
            var connectionString = "Server=localhost,1433;Database=csce361;User Id=sa;Password=Placeh0lder!Passw0rd;TrustServerCertificate=True;";
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                name: "_MyAllowSubdomainPolicy",
                policy =>
                {  
                    policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                }
                );
            });



            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddScoped<UserManager>();
            builder.Services.AddScoped<IUserAccessor, UserAccessor>();

            builder.Services.AddScoped<ICartItemAccessor, CartItemAccessor>();
            builder.Services.AddScoped<CartItemAccessor>();
            
            builder.Services.AddScoped<ICartAccessor, CartAccessor>();
            builder.Services.AddScoped<CartManager>();
            
            


            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.

            app.UseCors("_MyAllowSubdomainPolicy");


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
