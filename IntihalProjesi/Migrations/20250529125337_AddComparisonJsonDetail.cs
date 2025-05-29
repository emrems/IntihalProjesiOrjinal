using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntihalProjesi.Migrations
{
    /// <inheritdoc />
    public partial class AddComparisonJsonDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComparisonJsonDetail",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    FirstFileId = table.Column<int>(type: "int", nullable: false),
                    SecondFileId = table.Column<int>(type: "int", nullable: false),
                    JsonFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonJsonDetail", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_ComparisonJsonDetail_Dosyalar_FirstFileId",
                        column: x => x.FirstFileId,
                        principalTable: "Dosyalar",
                        principalColumn: "DosyaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComparisonJsonDetail_Dosyalar_SecondFileId",
                        column: x => x.SecondFileId,
                        principalTable: "Dosyalar",
                        principalColumn: "DosyaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComparisonJsonDetail_Icerikler_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Icerikler",
                        principalColumn: "IcerikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonJsonDetail_ContentId",
                table: "ComparisonJsonDetail",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonJsonDetail_FirstFileId",
                table: "ComparisonJsonDetail",
                column: "FirstFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonJsonDetail_SecondFileId",
                table: "ComparisonJsonDetail",
                column: "SecondFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComparisonJsonDetail");
        }
    }
}
