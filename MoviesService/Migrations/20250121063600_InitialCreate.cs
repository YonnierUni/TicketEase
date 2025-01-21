using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketEase.Service.Movies.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cast = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Cast", "Director", "Genre", "PosterImage", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"), "Robert Downey Jr., Chris Evans, Scarlett Johansson", "Anthony Russo, Joe Russo", "Action", "endgame.jpg", "Avengers: Endgame", 2019 },
                    { new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"), "Tom Holland, Zendaya, Benedict Cumberbatch", "Jon Watts", "Action, Sci-Fi", "spiderman_no_way_home.jpg", "Spider-Man: No Way Home", 2021 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
