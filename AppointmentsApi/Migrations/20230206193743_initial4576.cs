using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsApi.Migrations
{
    public partial class initial4576 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeEnd",
                table: "Appointments",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "TimeBegin",
                table: "Appointments",
                newName: "Begin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "End",
                table: "Appointments",
                newName: "TimeEnd");

            migrationBuilder.RenameColumn(
                name: "Begin",
                table: "Appointments",
                newName: "TimeBegin");
        }
    }
}
