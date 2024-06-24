using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class AddNumbOfAttendanceWarnings_numberOfAbsentDayToWarn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumbOfAttendanceWarnings",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numberOfAbsentDayToWarn",
                table: "SchoolInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumbOfAttendanceWarnings",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "numberOfAbsentDayToWarn",
                table: "SchoolInfo");
        }
    }
}
