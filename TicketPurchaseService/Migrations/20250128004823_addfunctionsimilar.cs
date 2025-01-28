using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketEase.Service.TicketPurchase.Migrations
{
    /// <inheritdoc />
    public partial class addfunctionsimilar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("15b49d93-cf74-4f7e-8394-6c75fe54685c"));

            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("d5a91f8e-8254-4d1d-bf32-f1d0282a5416"));

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "FunctionId", "FunctionDate", "MovieId", "Price" },
                values: new object[,]
                {
                    { new Guid("2a943434-f25e-4233-97b8-0135de632951"), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), 50.00m },
                    { new Guid("3b0d79ff-a7e0-4d40-b594-44a49f380634"), new DateTime(2025, 1, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), 65.00m },
                    { new Guid("44b2a7b7-f0ff-4a74-8bbf-856c19abfdb9"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"), 65.00m },
                    { new Guid("57c843b9-b8b0-493c-bd99-6497aa1eec1a"), new DateTime(2025, 1, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), 65.00m },
                    { new Guid("6e8b819f-76c2-402f-aaba-3fba1a79a06d"), new DateTime(2025, 1, 25, 16, 30, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), 65.00m },
                    { new Guid("9a271081-4581-43b9-b97d-1419db615f43"), new DateTime(2025, 1, 25, 21, 45, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), 65.00m }
                });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: new Guid("cfb88e29-4744-48c0-94fa-b25b92dea318"),
                column: "FunctionId",
                value: new Guid("44b2a7b7-f0ff-4a74-8bbf-856c19abfdb9"));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: new Guid("cfb88e29-4744-48c0-94fa-b25b92dea319"),
                column: "FunctionId",
                value: new Guid("2a943434-f25e-4233-97b8-0135de632951"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("2a943434-f25e-4233-97b8-0135de632951"));

            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("3b0d79ff-a7e0-4d40-b594-44a49f380634"));

            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("44b2a7b7-f0ff-4a74-8bbf-856c19abfdb9"));

            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("57c843b9-b8b0-493c-bd99-6497aa1eec1a"));

            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("6e8b819f-76c2-402f-aaba-3fba1a79a06d"));

            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "FunctionId",
                keyValue: new Guid("9a271081-4581-43b9-b97d-1419db615f43"));

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "FunctionId", "FunctionDate", "MovieId", "Price" },
                values: new object[,]
                {
                    { new Guid("15b49d93-cf74-4f7e-8394-6c75fe54685c"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"), 65.00m },
                    { new Guid("d5a91f8e-8254-4d1d-bf32-f1d0282a5416"), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), 50.00m }
                });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: new Guid("cfb88e29-4744-48c0-94fa-b25b92dea318"),
                column: "FunctionId",
                value: new Guid("15b49d93-cf74-4f7e-8394-6c75fe54685c"));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: new Guid("cfb88e29-4744-48c0-94fa-b25b92dea319"),
                column: "FunctionId",
                value: new Guid("d5a91f8e-8254-4d1d-bf32-f1d0282a5416"));
        }
    }
}
