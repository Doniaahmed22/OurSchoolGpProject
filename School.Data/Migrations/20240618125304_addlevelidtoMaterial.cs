using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class addlevelidtoMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Levelid",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_Levelid",
                table: "Materials",
                column: "Levelid");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Levels_Levelid",
                table: "Materials",
                column: "Levelid",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Levels_Levelid",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_Levelid",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Levelid",
                table: "Materials");
        }
    }
}
