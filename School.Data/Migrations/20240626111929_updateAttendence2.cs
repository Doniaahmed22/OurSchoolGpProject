using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class updateAttendence2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Announcements_AnnouncementId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Announcements_AnnouncementId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_AnnouncementId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Classes_AnnouncementId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "AnnouncementId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "AnnouncementId",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "Subjects",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnnouncementClasses",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementClasses", x => new { x.AnnouncementId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_AnnouncementClasses_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementClasses");

            migrationBuilder.DropColumn(
                name: "Subjects",
                table: "Announcements");

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_AnnouncementId",
                table: "Subjects",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AnnouncementId",
                table: "Classes",
                column: "AnnouncementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Announcements_AnnouncementId",
                table: "Classes",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Announcements_AnnouncementId",
                table: "Subjects",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id");
        }
    }
}
