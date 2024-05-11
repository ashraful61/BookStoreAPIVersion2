using BookSrote.API.Repository;
using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Build configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Specify the path to your appsettings.json file
                .Build();
            var connectionString = configuration.GetConnectionString("BookStoreDB");

            // Add services to the container.
            //builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.;Database=BookStoreAPINew;Integrated Security=True;TrustServerCertificate=True"));
            builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IBookRepository, BookRepository>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}