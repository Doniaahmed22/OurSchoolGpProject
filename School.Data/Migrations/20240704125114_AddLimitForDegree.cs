using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class AddLimitForDegree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinalDegree",
                table: "SchoolInfo",
                type: "int",
                nullable: false,
                defaultValue: 60);

            migrationBuilder.AddColumn<int>(
                name: "Midterm",
                table: "SchoolInfo",
                type: "int",
                nullable: false,
                defaultValue: 20);

            migrationBuilder.AddColumn<int>(
                name: "Workyear",
                table: "SchoolInfo",
                type: "int",
                nullable: false,
                defaultValue: 20);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "FinalDegree",
                table: "SchoolInfo");

            migrationBuilder.DropColumn(
                name: "Midterm",
                table: "SchoolInfo");

            migrationBuilder.DropColumn(
                name: "Workyear",
                table: "SchoolInfo");

        }
    }
}
