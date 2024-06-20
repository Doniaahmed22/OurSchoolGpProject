using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class AddCurrentTermToSchooInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTerm",
                table: "SchoolInfo",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolInfo_CurrentTerm",
                table: "SchoolInfo",
                column: "CurrentTerm");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolInfo_Terms_CurrentTerm",
                table: "SchoolInfo",
                column: "CurrentTerm",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolInfo_Terms_CurrentTerm",
                table: "SchoolInfo");

            migrationBuilder.DropIndex(
                name: "IX_SchoolInfo_CurrentTerm",
                table: "SchoolInfo");

            migrationBuilder.DropColumn(
                name: "CurrentTerm",
                table: "SchoolInfo");
        }
    }
}
