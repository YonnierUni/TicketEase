using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TicketEase.Service.Movies.Entities;

namespace TicketEase.Service.Movies.DbContexts
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                MovieId = Guid.Parse("7A573BF8-D473-4291-B221-B39B7CA415C9"),
                Title = "Avengers: Endgame",
                Year = 2019,
                Genre = "Action",
                Director = "Anthony Russo, Joe Russo",
                Cast = "Robert Downey Jr., Chris Evans, Scarlett Johansson",
                PosterImage = "https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_FMjpg_UX1000_.jpg"
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                MovieId = Guid.Parse("D12F5F9E-05A3-4742-8ECF-174C4AE57527"),
                Title = "Spider-Man: No Way Home",
                Year = 2021,
                Genre = "Action, Sci-Fi",
                Director = "Jon Watts",
                Cast = "Tom Holland, Zendaya, Benedict Cumberbatch",
                PosterImage = "https://m.media-amazon.com/images/M/MV5BMmFiZGZjMmEtMTA0Ni00MzA2LTljMTYtZGI2MGJmZWYzZTQ2XkEyXkFqcGc@._V1_.jpg"
            });
        }
    }
}