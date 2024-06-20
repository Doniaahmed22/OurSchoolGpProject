using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class addDefaultImageToSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Default.jpeg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Default.jpeg");
        }
    }
}
