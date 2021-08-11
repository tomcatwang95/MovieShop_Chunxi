using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Data
{
   public class MovieShopDbContext: DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
        {

        }

//		Code First Approach
//	Install the required packages

//	Creating the database using EF core code first approach, we use something called Migrations(set of commands that we run to create the database)
	
//	2 ways we can implement migrations
//		1.Universal Way => everyone Windows/Mac/Linux => dotnet cli
//		2.Windows and Visual Studio => Package Manager Console => here we can run our Migration commands
//		Create a connection string in your startup(MVC) project

//2 most important classes

//1. DbContext => Represents your Database
//2. DbSet => Your Tables

//Create a class that inherites from DbContext
// DbSets represents your tables
// Create the DbSets Properties inside DBContext
// Inject the COnnectionString from the Startup file (read the connection string from appsetting.json) to DbContext using DbContextOptions
// Migrations, run migrations against the DbContext Class which is located in Infrastructure
// COmmands that we are gonna tell Entity Framework to read our DbCOntext, DbSets, enttiies ,properties..
// Make sure Migrations are named in a meaningful way, think of them as SQL Scripts
// Add your very first migration usig Add-Migration InitialCreate 
// ALways check the created Migration FIle, to make sure it has things you are expecting. It has 2 methods Up() and Down()

   // Using Data Annotations, attributes you use on your Entities
   // Fluent API (more flecivle and has more options in advanced scenarios)
		public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Cast> Casts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasMany(m=> m.Genres).WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>("MovieGenre", 
                m=> m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));

            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new {mc.CastId, mc.MovieId, mc.Character });
            builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.CastId);

        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(2084);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // User Fluent API Rules

            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Ignore(m => m.Rating);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
        }

    }
}
