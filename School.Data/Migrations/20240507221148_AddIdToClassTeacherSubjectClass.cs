using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class AddIdToClassTeacherSubjectClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubjectClasses",
                table: "TeacherSubjectClasses");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeacherSubjectClasses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubjectClasses",
                table: "TeacherSubjectClasses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectClasses_ClassId",
                table: "TeacherSubjectClasses",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubjectClasses",
                table: "TeacherSubjectClasses");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjectClasses_ClassId",
                table: "TeacherSubjectClasses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeacherSubjectClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubjectClasses",
                table: "TeacherSubjectClasses",
                columns: new[] { "ClassId", "TeacherId", "SubjectId" });
        }
    }
}
