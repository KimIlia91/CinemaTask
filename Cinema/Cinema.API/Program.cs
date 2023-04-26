
using Cinema.API.Data;
using Cinema.API.Repositories.IRepositories;
using Cinema.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Cinema.API.Data.MappingConfigurations;
using Cinema.API.Services.IServices;
using Cinema.API.Services;

namespace Cinema.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(
               builder.Configuration.GetConnectionString("DefualtConnection")));

            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddScoped<IApiResponseFactory, ApiResponseFactory>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IDirectorService, DirectorService>();
            builder.Services.AddScoped<IScreenwriterService, ScreenwriterService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}