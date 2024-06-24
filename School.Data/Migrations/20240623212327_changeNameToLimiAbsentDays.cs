using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class changeNameToLimiAbsentDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numberOfAbsentDayToWarn",
                table: "SchoolInfo",
                newName: "LimitAbsentDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LimitAbsentDays",
                table: "SchoolInfo",
                newName: "numberOfAbsentDayToWarn");
        }
    }
}
