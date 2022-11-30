using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficesApi.Migrations
{
    public partial class init2349 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Offices",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Offices",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
