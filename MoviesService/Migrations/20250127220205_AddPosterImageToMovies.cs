using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketEase.Service.Movies.Migrations
{
    /// <inheritdoc />
    public partial class AddPosterImageToMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                column: "PosterImage",
                value: "https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_FMjpg_UX1000_.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"),
                column: "PosterImage",
                value: "https://m.media-amazon.com/images/M/MV5BMmFiZGZjMmEtMTA0Ni00MzA2LTljMTYtZGI2MGJmZWYzZTQ2XkEyXkFqcGc@._V1_.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                column: "PosterImage",
                value: "endgame.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"),
                column: "PosterImage",
                value: "spiderman_no_way_home.jpg");
        }
    }
}
