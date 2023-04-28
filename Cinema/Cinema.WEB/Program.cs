using Cinema.WEB.Data;
using Cinema.WEB.Services;
using Cinema.WEB.Services.IServices;

namespace Cinema.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IDirectorService, DirectorService>();
            builder.Services.AddScoped<IScreenwriterService, ScreenwriterService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IVideoService, VideoService>();

            builder.Services.AddHttpClient<ICinemaApiHttpClientService, CinemaApiHttpClientService>();
            builder.Services.AddScoped<ICinemaApiHttpClientService, CinemaApiHttpClientService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(30);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}