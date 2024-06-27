using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class UpdateAnnouncements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "ForWhich",
                table: "Announcements",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ForWhich",
                table: "Announcements");
        }
    }
}
