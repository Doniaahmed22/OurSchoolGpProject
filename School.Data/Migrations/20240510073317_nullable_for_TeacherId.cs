using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class nullable_for_TeacherId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectClasses_Teachers_TeacherId",
                table: "TeacherSubjectClasses");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "TeacherSubjectClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectClasses_Teachers_TeacherId",
                table: "TeacherSubjectClasses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjectClasses_Teachers_TeacherId",
                table: "TeacherSubjectClasses");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "TeacherSubjectClasses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjectClasses_Teachers_TeacherId",
                table: "TeacherSubjectClasses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
