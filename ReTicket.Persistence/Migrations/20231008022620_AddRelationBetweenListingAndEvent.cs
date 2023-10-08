using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReTicket.Persistence.Migrations
{
    public partial class AddRelationBetweenListingAndEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                schema: "dbo",
                table: "TicketListings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketListings_EventId",
                schema: "dbo",
                table: "TicketListings",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketListings_Events_EventId",
                schema: "dbo",
                table: "TicketListings",
                column: "EventId",
                principalSchema: "dbo",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketListings_Events_EventId",
                schema: "dbo",
                table: "TicketListings");

            migrationBuilder.DropIndex(
                name: "IX_TicketListings_EventId",
                schema: "dbo",
                table: "TicketListings");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "dbo",
                table: "TicketListings");
        }
    }
}
