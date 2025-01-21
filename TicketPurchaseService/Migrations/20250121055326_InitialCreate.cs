using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketEase.Service.TicketPurchase.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    FunctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FunctionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.FunctionId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FunctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "FunctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "FunctionId", "FunctionDate", "MovieId", "Price" },
                values: new object[,]
                {
                    { new Guid("15b49d93-cf74-4f7e-8394-6c75fe54685c"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"), 65.00m },
                    { new Guid("d5a91f8e-8254-4d1d-bf32-f1d0282a5416"), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), 50.00m }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "AdditionalPrice", "FunctionId", "UserName" },
                values: new object[,]
                {
                    { new Guid("cfb88e29-4744-48c0-94fa-b25b92dea318"), 15.00m, new Guid("15b49d93-cf74-4f7e-8394-6c75fe54685c"), "JaneSmith" },
                    { new Guid("cfb88e29-4744-48c0-94fa-b25b92dea319"), 10.00m, new Guid("d5a91f8e-8254-4d1d-bf32-f1d0282a5416"), "JohnDoe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FunctionId",
                table: "Tickets",
                column: "FunctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Functions");
        }
    }
}
