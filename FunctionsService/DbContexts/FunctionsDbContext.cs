using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using TicketEase.Service.Functions.Entities;

namespace TicketEase.Service.Functions.DbContexts
{
    public class FunctionsDbContext : DbContext
    {
        public FunctionsDbContext(DbContextOptions<FunctionsDbContext> options) : base(options)
        {
        }

        public DbSet<MovieFunction> MovieFunctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieFunction>().HasData(new MovieFunction
            {
                MovieFunctionId = Guid.Parse("2A943434-F25E-4233-97B8-0135DE632951"),
                MovieId = Guid.Parse("7A573BF8-D473-4291-B221-B39B7CA415C9"),
                Room = "Room 1",
                StartTime = new DateTime(2025, 1, 25, 19, 0, 0),
                EndTime = new DateTime(2025, 1, 25, 21, 30, 0),
                TotalSeats = 100,
                AvailableSeats = 100
            });

            modelBuilder.Entity<MovieFunction>().HasData(new MovieFunction
            {
                MovieFunctionId = Guid.Parse("44B2A7B7-F0FF-4A74-8BBF-856C19ABFDB9"),
                MovieId = Guid.Parse("D12F5F9E-05A3-4742-8ECF-174C4AE57527"),
                Room = "Room 2",
                StartTime = new DateTime(2025, 1, 26, 15, 0, 0),
                EndTime = new DateTime(2025, 1, 26, 17, 30, 0),
                TotalSeats = 150,
                AvailableSeats = 150
            });
        }
    }
}
