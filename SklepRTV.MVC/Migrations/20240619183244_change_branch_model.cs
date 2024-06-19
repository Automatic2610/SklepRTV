using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepRTV.MVC.Migrations
{
    /// <inheritdoc />
    public partial class change_branch_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "countryId",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "Branches",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Branches",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "houseNo",
                table: "Branches",
                newName: "HouseNo");

            migrationBuilder.RenameColumn(
                name: "flatNo",
                table: "Branches",
                newName: "FlatNo");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Branches",
                newName: "City");

            migrationBuilder.AlterColumn<int>(
                name: "FlatNo",
                table: "Branches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Branches",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Branches",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "HouseNo",
                table: "Branches",
                newName: "houseNo");

            migrationBuilder.RenameColumn(
                name: "FlatNo",
                table: "Branches",
                newName: "flatNo");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Branches",
                newName: "city");

            migrationBuilder.AlterColumn<int>(
                name: "flatNo",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "countryId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
