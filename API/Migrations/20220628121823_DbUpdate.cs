using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class DbUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Photos",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Photos",
                newName: "Publicid");

            migrationBuilder.RenameColumn(
                name: "MainPicture",
                table: "Photos",
                newName: "isMain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "Publicid",
                table: "Photos",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "isMain",
                table: "Photos",
                newName: "MainPicture");
        }
    }
}
