using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class fixedInternalUserStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActived",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Staffs");

            migrationBuilder.AddColumn<bool>(
                name: "IsActived",
                table: "Staffs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActived",
                table: "Staffs");

            migrationBuilder.AddColumn<bool>(
                name: "IsActived",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
