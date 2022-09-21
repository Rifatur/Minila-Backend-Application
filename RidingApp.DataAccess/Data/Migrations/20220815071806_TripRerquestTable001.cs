using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RidingApp.DataAccess.Data.Migrations
{
    public partial class TripRerquestTable001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripRequest",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudetID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChauffeurId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    road = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripRequest", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripRequest");
        }
    }
}
