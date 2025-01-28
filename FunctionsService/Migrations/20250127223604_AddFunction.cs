using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketEase.Service.Functions.Migrations
{
    /// <inheritdoc />
    public partial class AddFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MovieFunctions",
                columns: new[] { "MovieFunctionId", "AvailableSeats", "EndTime", "MovieId", "Room", "StartTime", "TotalSeats" },
                values: new object[,]
                {
                    { new Guid("3b0d79ff-a7e0-4d40-b594-44a49f380634"), 100, new DateTime(2025, 1, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), "Room 1", new DateTime(2025, 1, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { new Guid("57c843b9-b8b0-493c-bd99-6497aa1eec1a"), 100, new DateTime(2025, 1, 25, 16, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), "Room 1", new DateTime(2025, 1, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), 100 },
                    { new Guid("6e8b819f-76c2-402f-aaba-3fba1a79a06d"), 100, new DateTime(2025, 1, 25, 19, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), "Room 1", new DateTime(2025, 1, 25, 16, 30, 0, 0, DateTimeKind.Unspecified), 100 },
                    { new Guid("9a271081-4581-43b9-b97d-1419db615f43"), 100, new DateTime(2025, 1, 25, 23, 45, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), "Room 1", new DateTime(2025, 1, 25, 21, 45, 0, 0, DateTimeKind.Unspecified), 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MovieFunctions",
                keyColumn: "MovieFunctionId",
                keyValue: new Guid("3b0d79ff-a7e0-4d40-b594-44a49f380634"));

            migrationBuilder.DeleteData(
                table: "MovieFunctions",
                keyColumn: "MovieFunctionId",
                keyValue: new Guid("57c843b9-b8b0-493c-bd99-6497aa1eec1a"));

            migrationBuilder.DeleteData(
                table: "MovieFunctions",
                keyColumn: "MovieFunctionId",
                keyValue: new Guid("6e8b819f-76c2-402f-aaba-3fba1a79a06d"));

            migrationBuilder.DeleteData(
                table: "MovieFunctions",
                keyColumn: "MovieFunctionId",
                keyValue: new Guid("9a271081-4581-43b9-b97d-1419db615f43"));
        }
    }
}
