using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectPS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DoctorsAreAssignedToCenters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransfusionCenterId",
                table: "Doctor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_TransfusionCenterId",
                table: "Doctor",
                column: "TransfusionCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_TransfusionCenter_TransfusionCenterId",
                table: "Doctor",
                column: "TransfusionCenterId",
                principalTable: "TransfusionCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_TransfusionCenter_TransfusionCenterId",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_TransfusionCenterId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "TransfusionCenterId",
                table: "Doctor");
        }
    }
}
