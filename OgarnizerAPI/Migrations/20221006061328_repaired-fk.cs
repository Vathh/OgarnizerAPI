using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgarnizerAPI.Migrations
{
    public partial class repairedfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices");

            migrationBuilder.DropIndex(
                name: "IX_ClosedServices_CloseUserId",
                table: "ClosedServices");

            migrationBuilder.DropIndex(
                name: "IX_ClosedOrders_CloseUserId",
                table: "ClosedOrders");

            migrationBuilder.AddColumn<int>(
                name: "FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClosedServices_FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices",
                column: "FK_ClosedServices_Users_CloseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedOrders_FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders",
                column: "FK_ClosedOrders_Users_CloseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedOrders_Users_FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders",
                column: "FK_ClosedOrders_Users_CloseUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedServices_Users_FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices",
                column: "FK_ClosedServices_Users_CloseUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClosedOrders_Users_FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ClosedServices_Users_FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices");

            migrationBuilder.DropIndex(
                name: "IX_ClosedServices_FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices");

            migrationBuilder.DropIndex(
                name: "IX_ClosedOrders_FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders");

            migrationBuilder.DropColumn(
                name: "FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices");

            migrationBuilder.DropColumn(
                name: "FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedServices_CloseUserId",
                table: "ClosedServices",
                column: "CloseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosedOrders_CloseUserId",
                table: "ClosedOrders",
                column: "CloseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedOrders_Users_CloseUserId",
                table: "ClosedOrders",
                column: "CloseUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedServices_Users_CloseUserId",
                table: "ClosedServices",
                column: "CloseUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
