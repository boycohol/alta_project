using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class update_FK_Guest_GuestGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_GuestGroups_GuestGroupId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_GuestGroupId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "GuestGroupId",
                table: "Guests");

            migrationBuilder.CreateTable(
                name: "Guest_GuestGroup",
                columns: table => new
                {
                    GuestGroupsId = table.Column<int>(type: "integer", nullable: false),
                    GuestsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest_GuestGroup", x => new { x.GuestGroupsId, x.GuestsId });
                    table.ForeignKey(
                        name: "FK_Guest_GuestGroup_GuestGroups_GuestGroupsId",
                        column: x => x.GuestGroupsId,
                        principalTable: "GuestGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guest_GuestGroup_Guests_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guest_GuestGroup_GuestsId",
                table: "Guest_GuestGroup",
                column: "GuestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guest_GuestGroup");

            migrationBuilder.AddColumn<int>(
                name: "GuestGroupId",
                table: "Guests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_GuestGroupId",
                table: "Guests",
                column: "GuestGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_GuestGroups_GuestGroupId",
                table: "Guests",
                column: "GuestGroupId",
                principalTable: "GuestGroups",
                principalColumn: "Id");
        }
    }
}
