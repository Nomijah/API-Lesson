using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lektion_SUT24_250414_API_intro.Migrations
{
    /// <inheritdoc />
    public partial class AddedDirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "BirthYear", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 1970, "Christopher", "Nolan" },
                    { 2, 1939, "Francis", "Ford Coppola" }
                });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "DirectorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "DirectorId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "DirectorId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Director",
                value: "Christopher Nolan");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Director",
                value: "Francis Ford Coppola");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Director",
                value: "Christopher Nolan");
        }
    }
}
