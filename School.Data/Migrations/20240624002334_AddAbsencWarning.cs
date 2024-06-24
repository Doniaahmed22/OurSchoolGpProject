using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class AddAbsencWarning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumbOfAttendanceWarnings",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "AbsenceWarnings",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    WarningDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceWarnings", x => new { x.StudentId, x.WarningDate });
                    table.ForeignKey(
                        name: "FK_AbsenceWarnings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceWarnings");

            migrationBuilder.AddColumn<int>(
                name: "NumbOfAttendanceWarnings",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
