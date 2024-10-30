using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto01.Migrations
{
    public partial class UpdatingAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointments",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Appointments",
                newName: "AppointmentDate");
        }
    }
}
