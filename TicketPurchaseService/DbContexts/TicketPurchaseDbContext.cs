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

            var function1Id = Guid.Parse("{D5A91F8E-8254-4D1D-BF32-F1D0282A5416}");
            var function2Id = Guid.Parse("{15B49D93-CF74-4F7E-8394-6C75FE54685C}");

            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function1Id,
                MovieId = Guid.Parse("7A573BF8-D473-4291-B221-B39B7CA415C9"),
                Price = 50.00m,
                FunctionDate = new DateTime(2025, 3, 1)
            });

            modelBuilder.Entity<Function>().HasData(new Function
            {
                FunctionId = function2Id,
                MovieId = Guid.Parse("D12F5F9E-05A3-4742-8ECF-174C4AE57527"),
                Price = 65.00m,
                FunctionDate = new DateTime(2025, 4, 1)
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

