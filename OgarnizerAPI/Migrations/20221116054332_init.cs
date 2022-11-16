using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OgarnizerAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForRelease",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToInvoice",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ForRelease",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToInvoice",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToInvoice",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CloseUserId",
                table: "ClosedServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "ForRelease",
                table: "ClosedServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToInvoice",
                table: "ClosedServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CloseUserId",
                table: "ClosedOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "ForRelease",
                table: "ClosedOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToInvoice",
                table: "ClosedOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToInvoice",
                table: "ClosedJobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForRelease",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ToInvoice",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ForRelease",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ToInvoice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ToInvoice",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ForRelease",
                table: "ClosedServices");

            migrationBuilder.DropColumn(
                name: "ToInvoice",
                table: "ClosedServices");

            migrationBuilder.DropColumn(
                name: "ForRelease",
                table: "ClosedOrders");

            migrationBuilder.DropColumn(
                name: "ToInvoice",
                table: "ClosedOrders");

            migrationBuilder.DropColumn(
                name: "ToInvoice",
                table: "ClosedJobs");

            migrationBuilder.AlterColumn<int>(
                name: "CloseUserId",
                table: "ClosedServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CloseUserId",
                table: "ClosedOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
