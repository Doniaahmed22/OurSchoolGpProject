using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class MakeIspresentAttendanceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAttendence",
                table: "Attendences");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceType",
                table: "Attendences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceType",
                table: "Attendences");

            migrationBuilder.AddColumn<bool>(
                name: "IsAttendence",
                table: "Attendences",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
