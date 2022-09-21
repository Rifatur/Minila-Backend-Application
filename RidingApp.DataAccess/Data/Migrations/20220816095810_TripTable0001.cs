using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RidingApp.DataAccess.Data.Migrations
{
    public partial class TripTable0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tripCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tripStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tripStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tripEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudetID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cancelReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startLocation = table.Column<int>(type: "int", nullable: false),
                    endLocation = table.Column<int>(type: "int", nullable: false),
                    DriverRating = table.Column<int>(type: "int", nullable: false),
                    StudetRating = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trip");
        }
    }
}
