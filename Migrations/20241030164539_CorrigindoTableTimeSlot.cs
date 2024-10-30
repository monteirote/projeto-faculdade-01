using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto01.Migrations
{
    public partial class CorrigindoTableTimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvaliable",
                table: "TimeSlots",
                newName: "IsAvailable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "TimeSlots",
                newName: "IsAvaliable");
        }
    }
}
