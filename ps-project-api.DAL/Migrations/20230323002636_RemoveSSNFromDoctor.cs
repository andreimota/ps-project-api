using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectPS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSSNFromDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Donor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Donor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
