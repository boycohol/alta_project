using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class remove_GuestGroupEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guest_GuestGroup");

            migrationBuilder.DropTable(
                name: "GuestGroups");

            migrationBuilder.CreateTable(
                name: "VisitPlan_Guest",
                columns: table => new
                {
                    GuestsId = table.Column<int>(type: "integer", nullable: false),
                    VisitPlansId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitPlan_Guest", x => new { x.GuestsId, x.VisitPlansId });
                    table.ForeignKey(
                        name: "FK_VisitPlan_Guest_Guests_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitPlan_Guest_VisitPlans_VisitPlansId",
                        column: x => x.VisitPlansId,
                        principalTable: "VisitPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitPlan_Guest_VisitPlansId",
                table: "VisitPlan_Guest",
                column: "VisitPlansId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitPlan_Guest");

            migrationBuilder.CreateTable(
                name: "GuestGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitPlan_GuestGroup",
                        column: x => x.Id,
                        principalTable: "VisitPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
    }
}
