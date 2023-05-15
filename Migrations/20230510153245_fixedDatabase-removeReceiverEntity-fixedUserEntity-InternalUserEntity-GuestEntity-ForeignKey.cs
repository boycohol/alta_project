using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AltaProject.Migrations
{
    public partial class fixedDatabaseremoveReceiverEntityfixedUserEntityInternalUserEntityGuestEntityForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalUser_Notification",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Receivers");

            migrationBuilder.AddColumn<int>(
                name: "UserReceiverId",
                table: "Notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserReceiverId",
                table: "Notifications",
                column: "UserReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalUser_SendedNotification",
                table: "Notifications",
                column: "SenderUserId",
                principalTable: "InternalUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ReceiverNotification",
                table: "Notifications",
                column: "UserReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalUser_SendedNotification",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ReceiverNotification",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserReceiverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserReceiverId",
                table: "Notifications");

            migrationBuilder.CreateTable(
                name: "Receivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuestId = table.Column<int>(type: "integer", nullable: true),
                    InternalUserId = table.Column<int>(type: "integer", nullable: true),
                    NotificationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guest_Receiver",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternalUser_Receiver",
                        column: x => x.InternalUserId,
                        principalTable: "InternalUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notification_Receiver",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_GuestId",
                table: "Receivers",
                column: "GuestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_InternalUserId",
                table: "Receivers",
                column: "InternalUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_NotificationId",
                table: "Receivers",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalUser_Notification",
                table: "Notifications",
                column: "SenderUserId",
                principalTable: "InternalUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
