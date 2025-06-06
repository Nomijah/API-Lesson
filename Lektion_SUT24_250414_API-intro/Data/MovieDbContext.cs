﻿using Lektion_SUT24_250414_API_intro.Models;
using Microsoft.EntityFrameworkCore;

namespace Lektion_SUT24_250414_API_intro.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Directors
            modelBuilder.Entity<Director>().HasData(
                new Director(1, "Christopher", "Nolan", 1970),
                new Director(2, "Francis", "Ford Coppola", 1939)
            );

            // Seed Actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, FirstName = "Leonardo", LastName = "DiCaprio", BirthYear = 1974 },
                new Actor { Id = 2, FirstName = "Joseph", LastName = "Gordon-Levitt", BirthYear = 1981 },
                new Actor { Id = 3, FirstName = "Elliot", LastName = "Page", BirthYear = 1987 },
                new Actor { Id = 4, FirstName = "Marlon", LastName = "Brando", BirthYear = 1924 },
                new Actor { Id = 5, FirstName = "Al", LastName = "Pacino", BirthYear = 1940 },
                new Actor { Id = 6, FirstName = "James", LastName = "Caan", BirthYear = 1940 },
                new Actor { Id = 7, FirstName = "Christian", LastName = "Bale", BirthYear = 1974 },
                new Actor { Id = 8, FirstName = "Heath", LastName = "Ledger", BirthYear = 1979 },
                new Actor { Id = 9, FirstName = "Aaron", LastName = "Eckhart", BirthYear = 1968 }
            );

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Length = TimeSpan.FromMinutes(148),
                    Genre = "Science Fiction",
                    DirectorId = 1
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Godfather",
                    Length = TimeSpan.FromMinutes(175),
                    Genre = "Crime",
                    DirectorId = 2
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Dark Knight",
                    Length = TimeSpan.FromMinutes(152),
                    Genre = "Action",
                    DirectorId = 1
                }
            );

            // Seed Movie-Actor Relationships
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity(am => am.HasData(
                    new { MoviesId = 1, ActorsId = 1 },
                    new { MoviesId = 1, ActorsId = 2 },
                    new { MoviesId = 1, ActorsId = 3 },
                    new { MoviesId = 2, ActorsId = 4 },
                    new { MoviesId = 2, ActorsId = 5 },
                    new { MoviesId = 2, ActorsId = 6 },
                    new { MoviesId = 3, ActorsId = 7 },
                    new { MoviesId = 3, ActorsId = 8 },
                    new { MoviesId = 3, ActorsId = 9 }
                ));
        }

    }
}
