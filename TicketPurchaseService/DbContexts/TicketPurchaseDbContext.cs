using System.Collections.Generic;
using System.Reflection.Emit;
using TicketEase.Service.TicketPurchase.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketEase.Service.TicketPurchase.DbContexts
{
    public class TicketPurchaseDbContext : DbContext
    {
        public TicketPurchaseDbContext(DbContextOptions<TicketPurchaseDbContext> options) : base(options)
        {
        }

        public DbSet<Function> Functions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var function1Id = Guid.Parse("{2A943434-F25E-4233-97B8-0135DE632951}");
            var function2Id = Guid.Parse("{44B2A7B7-F0FF-4A74-8BBF-856C19ABFDB9}");
            var function3Id = Guid.Parse("{3B0D79FF-A7E0-4D40-B594-44A49F380634}");
            var function4Id = Guid.Parse("{57C843B9-B8B0-493C-BD99-6497AA1EEC1A}");
            var function5Id = Guid.Parse("{6E8B819F-76C2-402F-AABA-3FBA1A79A06D}");
            var function6Id = Guid.Parse("{9A271081-4581-43B9-B97D-1419DB615F43}");

            var movieId1 = Guid.Parse("{7A573BF8-D473-4291-B221-B39B7CA415C9}"); // Movie 1
            var movieId2 = Guid.Parse("{D12F5F9E-05A3-4742-8ECF-174C4AE57527}"); // Movie 2


            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function1Id,
                MovieId = movieId1,
                Price = 50.00m,
                FunctionDate = new DateTime(2025, 3, 1)
            });

            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function2Id,
                MovieId = movieId2,
                Price = 65.00m,
                FunctionDate = new DateTime(2025, 4, 1)
            });

            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function3Id,
                MovieId = movieId1,
                Price = 65.00m,
                FunctionDate = new DateTime(2025, 1, 25, 11, 0, 0)
            });

            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function4Id,
                MovieId = movieId1,
                Price = 65.00m,
                FunctionDate = new DateTime(2025, 1, 25, 13, 30, 0)
            });

            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function5Id,
                MovieId = movieId1,
                Price = 65.00m,
                FunctionDate = new DateTime(2025, 1, 25, 16, 30, 0)
            });

            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function6Id,
                MovieId = movieId1,
                Price = 65.00m,
                FunctionDate = new DateTime(2025, 1, 25, 21, 45, 0)
            });


            modelBuilder.Entity<Ticket>().HasData(new Ticket
            {
                TicketId = Guid.Parse("{CFB88E29-4744-48C0-94FA-B25B92DEA319}"),
                FunctionId = function1Id,
                AdditionalPrice = 10.00m,
                UserName = "JohnDoe"
            });

            modelBuilder.Entity<Ticket>().HasData(new Ticket
            {
                TicketId = Guid.Parse("{CFB88E29-4744-48C0-94FA-B25B92DEA318}"),
                FunctionId = function2Id,
                AdditionalPrice = 15.00m,
                UserName = "JaneSmith"
            });
        }
    }
}

