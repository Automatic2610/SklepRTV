using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepRTV.MVC.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imagePath",
                table: "Products",
                newName: "image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "Products",
                newName: "imagePath");
        }
    }
}
