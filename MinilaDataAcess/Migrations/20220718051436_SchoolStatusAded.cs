using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinilaDataAcess.Migrations
{
    public partial class SchoolStatusAded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "schools",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "schools");
        }
    }
}
