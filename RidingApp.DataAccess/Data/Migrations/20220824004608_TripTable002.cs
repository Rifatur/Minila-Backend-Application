using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RidingApp.DataAccess.Data.Migrations
{
    public partial class TripTable002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripRequestId",
                table: "Trip",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripRequestId",
                table: "Trip");
        }
    }
}
