using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketEase.Service.Functions.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieFunctions",
                columns: table => new
                {
                    MovieFunctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieFunctions", x => x.MovieFunctionId);
                });

            migrationBuilder.InsertData(
                table: "MovieFunctions",
                columns: new[] { "MovieFunctionId", "AvailableSeats", "EndTime", "MovieId", "Room", "StartTime", "TotalSeats" },
                values: new object[,]
                {
                    { new Guid("2a943434-f25e-4233-97b8-0135de632951"), 100, new DateTime(2025, 1, 25, 21, 30, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), "Room 1", new DateTime(2025, 1, 25, 19, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { new Guid("44b2a7b7-f0ff-4a74-8bbf-856c19abfdb9"), 150, new DateTime(2025, 1, 26, 17, 30, 0, 0, DateTimeKind.Unspecified), new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"), "Room 2", new DateTime(2025, 1, 26, 15, 0, 0, 0, DateTimeKind.Unspecified), 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieFunctions");
        }
    }
}
