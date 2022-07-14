using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinilaDataAcess.Migrations
{
    public partial class TripRequestTable : Migration
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
                    status = table.Column<int>(type: "int", nullable: false)
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
