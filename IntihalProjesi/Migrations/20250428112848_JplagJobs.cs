using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntihalProjesi.Migrations
{
    /// <inheritdoc />
    public partial class JplagJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JplagJobs",
                columns: table => new
                {
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IcerikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JplagJobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_JplagJobs_Icerikler_IcerikId",
                        column: x => x.IcerikId,
                        principalTable: "Icerikler",
                        principalColumn: "IcerikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JplagJobs_IcerikId",
                table: "JplagJobs",
                column: "IcerikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JplagJobs");
        }
    }
}
