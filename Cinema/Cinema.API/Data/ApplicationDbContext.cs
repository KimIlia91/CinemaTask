using Cinema.API.Models.CastModels;
using Cinema.API.Models.GenreModels;
using Cinema.API.Models.MovieModels;
using Cinema.API.Models.PersonModels;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Набор сущностей <see cref="Movie"/>
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Набор сущностей <see cref="Genre"/>
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Набор сущностей <see cref="Person"/>
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Набор сущностей <see cref="Actor"/>
        /// </summary>
        public DbSet<Actor> Actors { get; set; }

        /// <summary>
        /// Набор сущностей <see cref="Director"/>
        /// </summary>
        public DbSet<Director> Directors { get; set; }

        /// <summary>
        /// Набор сущностей <see cref="Screenwriter"/>
        /// </summary>
        public DbSet<Screenwriter> Screenwriters { get; set; }

        /// <summary>
        /// Конструктор класса ApplicationDbContext, который принимает настройки контекста базы данных.
        /// </summary>
        /// <param name="options">Настройки контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
