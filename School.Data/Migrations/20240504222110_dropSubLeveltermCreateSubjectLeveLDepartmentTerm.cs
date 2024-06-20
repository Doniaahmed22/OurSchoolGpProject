using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Data.Migrations
{
    public partial class dropSubLeveltermCreateSubjectLeveLDepartmentTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectDepartments");

            migrationBuilder.DropTable(
                name: "SubjectLevels");

            migrationBuilder.DropTable(
                name: "SubjectTerms");

            migrationBuilder.CreateTable(
                name: "SubjectLevelDepartmentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectLevelDepartmentTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectLevelDepartmentTerms_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectLevelDepartmentTerms_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectLevelDepartmentTerms_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectLevelDepartmentTerms_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevelDepartmentTerms_DepartmentId",
                table: "SubjectLevelDepartmentTerms",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevelDepartmentTerms_LevelId",
                table: "SubjectLevelDepartmentTerms",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevelDepartmentTerms_SubjectId",
                table: "SubjectLevelDepartmentTerms",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevelDepartmentTerms_TermId",
                table: "SubjectLevelDepartmentTerms",
                column: "TermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectLevelDepartmentTerms");

            migrationBuilder.CreateTable(
                name: "SubjectDepartments",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDepartments", x => new { x.SubjectId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_SubjectDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectDepartments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectLevels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectLevels", x => new { x.LevelId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_SubjectLevels_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectLevels_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTerms",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTerms", x => new { x.SubjectId, x.TermId });
                    table.ForeignKey(
                        name: "FK_SubjectTerms_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTerms_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDepartments_DepartmentId",
                table: "SubjectDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectLevels_SubjectId",
                table: "SubjectLevels",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTerms_TermId",
                table: "SubjectTerms",
                column: "TermId");
        }
    }
}
