using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgarnizerAPI.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs");

            migrationBuilder.DropIndex(
                name: "IX_ClosedJobs_CloseUserId",
                table: "ClosedJobs");

            migrationBuilder.AlterColumn<int>(
                name: "CloseUserId",
                table: "ClosedJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClosedJobs_FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs",
                column: "FK_ClosedJobs_Users_CloseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedJobs_Users_FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs",
                column: "FK_ClosedJobs_Users_CloseUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClosedJobs_Users_FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs");

            migrationBuilder.DropIndex(
                name: "IX_ClosedJobs_FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs");

            migrationBuilder.DropColumn(
                name: "FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs");

            migrationBuilder.AlterColumn<int>(
                name: "CloseUserId",
                table: "ClosedJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClosedJobs_CloseUserId",
                table: "ClosedJobs",
                column: "CloseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosedJobs_Users_CloseUserId",
                table: "ClosedJobs",
                column: "CloseUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
