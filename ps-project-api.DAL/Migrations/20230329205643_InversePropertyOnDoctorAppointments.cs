using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectPS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InversePropertyOnDoctorAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointment");
        }
    }
}
