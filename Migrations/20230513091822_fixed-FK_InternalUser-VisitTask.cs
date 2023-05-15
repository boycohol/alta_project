using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class fixedFK_InternalUserVisitTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalUser_VisitTask",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatorUserId",
                table: "Tasks",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalUser_VisitTask",
                table: "Tasks",
                column: "CreatorUserId",
                principalTable: "InternalUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalUser_VisitTask",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatorUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_InternalUser_VisitTask",
                table: "Tasks",
                column: "Id",
                principalTable: "InternalUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
