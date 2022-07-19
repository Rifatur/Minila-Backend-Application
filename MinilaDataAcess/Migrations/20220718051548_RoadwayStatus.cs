using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinilaDataAcess.Migrations
{
    public partial class RoadwayStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "roadWays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "roadWays");
        }
    }
}
