using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lektion_SUT24_250414_API_intro.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<TimeSpan>(type: "time", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actors = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Actors", "Director", "Genre", "Length", "Title" },
                values: new object[,]
                {
                    { 1, "[\"Leonardo DiCaprio\",\"Joseph Gordon-Levitt\",\"Elliot Page\"]", "Christopher Nolan", "Science Fiction", new TimeSpan(0, 2, 28, 0, 0), "Inception" },
                    { 2, "[\"Marlon Brando\",\"Al Pacino\",\"James Caan\"]", "Francis Ford Coppola", "Crime", new TimeSpan(0, 2, 55, 0, 0), "The Godfather" },
                    { 3, "[\"Christian Bale\",\"Heath Ledger\",\"Aaron Eckhart\"]", "Christopher Nolan", "Action", new TimeSpan(0, 2, 32, 0, 0), "The Dark Knight" }
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
