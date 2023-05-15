using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class fix_Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "CommentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_CommentUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentUserId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentUserId",
                table: "Comments",
                newName: "IX_Comments_UserId");
        }
    }
}
