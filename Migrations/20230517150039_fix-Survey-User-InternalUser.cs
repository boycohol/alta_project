using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class fixSurveyUserInternalUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_InternalUsers_ImplementUserId",
                table: "Surveys");

            migrationBuilder.RenameColumn(
                name: "ImplementUserId",
                table: "Surveys",
                newName: "CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_ImplementUserId",
                table: "Surveys",
                newName: "IX_Surveys_CreatorUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Surveys",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Surveys",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "User_Survey",
                columns: table => new
                {
                    ImplementUsersId = table.Column<int>(type: "integer", nullable: false),
                    SurveysId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Survey", x => new { x.ImplementUsersId, x.SurveysId });
                    table.ForeignKey(
                        name: "FK_User_Survey_Surveys_SurveysId",
                        column: x => x.SurveysId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Survey_Users_ImplementUsersId",
                        column: x => x.ImplementUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Survey_SurveysId",
                table: "User_Survey",
                column: "SurveysId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalUser_Survey",
                table: "Surveys",
                column: "CreatorUserId",
                principalTable: "InternalUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalUser_Survey",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "User_Survey");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "Surveys",
                newName: "ImplementUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_CreatorUserId",
                table: "Surveys",
                newName: "IX_Surveys_ImplementUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Surveys",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Surveys",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_InternalUsers_ImplementUserId",
                table: "Surveys",
                column: "ImplementUserId",
                principalTable: "InternalUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
