using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsApi.Migrations
{
    public partial class initial65 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAppointments_Appointments_AppointmentId",
                table: "DoctorAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_Appointments_AppointmentId",
                table: "PatientAppointments");

            migrationBuilder.DropIndex(
                name: "IX_PatientAppointments_AppointmentId",
                table: "PatientAppointments");

            migrationBuilder.DropIndex(
                name: "IX_DoctorAppointments_AppointmentId",
                table: "DoctorAppointments");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "PatientAppointments");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "DoctorAppointments");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorAppointmentId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PatientAppointmentId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorAppointmentId",
                table: "Appointments",
                column: "DoctorAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientAppointmentId",
                table: "Appointments",
                column: "PatientAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorAppointments_DoctorAppointmentId",
                table: "Appointments",
                column: "DoctorAppointmentId",
                principalTable: "DoctorAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PatientAppointments_PatientAppointmentId",
                table: "Appointments",
                column: "PatientAppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorAppointments_DoctorAppointmentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PatientAppointments_PatientAppointmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorAppointmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientAppointmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorAppointmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientAppointmentId",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "PatientAppointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "DoctorAppointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_AppointmentId",
                table: "PatientAppointments",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAppointments_AppointmentId",
                table: "DoctorAppointments",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAppointments_Appointments_AppointmentId",
                table: "DoctorAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_Appointments_AppointmentId",
                table: "PatientAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
