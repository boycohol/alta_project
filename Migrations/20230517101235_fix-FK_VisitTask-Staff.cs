using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class fixFK_VisitTaskStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeStaffId",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeStaffId",
                table: "Tasks",
                column: "AssigneeStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeStaffId",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeStaffId",
                table: "Tasks",
                column: "AssigneeStaffId",
                unique: true);
        }
    }
}
