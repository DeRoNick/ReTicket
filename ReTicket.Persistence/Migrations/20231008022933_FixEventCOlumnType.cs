using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReTicket.Persistence.Migrations
{
    public partial class FixEventCOlumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "money",
                schema: "dbo",
                table: "Events",
                newName: "TicketPrice");

            migrationBuilder.RenameColumn(
                name: "datetime",
                schema: "dbo",
                table: "Events",
                newName: "StartDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "TicketPrice",
                schema: "dbo",
                table: "Events",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "dbo",
                table: "Events",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "dbo",
                table: "Events",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "dbo",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "TicketPrice",
                schema: "dbo",
                table: "Events",
                newName: "money");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "dbo",
                table: "Events",
                newName: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "money",
                schema: "dbo",
                table: "Events",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<DateTime>(
                name: "datetime",
                schema: "dbo",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
