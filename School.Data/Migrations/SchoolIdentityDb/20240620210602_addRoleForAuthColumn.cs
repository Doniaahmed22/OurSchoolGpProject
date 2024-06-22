using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations.SchoolIdentityDb
{
    public partial class addRoleForAuthColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AspNetUsers",
                newName: "RoleForAuth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleForAuth",
                table: "AspNetUsers",
                newName: "Role");
        }
    }
}
