using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntihalProjesi.Migrations
{
    /// <inheritdoc />
    public partial class ilk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "Icerikler",
                columns: table => new
                {
                    IcerikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icerikler", x => x.IcerikId);
                    table.ForeignKey(
                        name: "FK_Icerikler_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dosyalar",
                columns: table => new
                {
                    DosyaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CleanedPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    IcerikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosyalar", x => x.DosyaId);
                    table.ForeignKey(
                        name: "FK_Dosyalar_Icerikler_IcerikId",
                        column: x => x.IcerikId,
                        principalTable: "Icerikler",
                        principalColumn: "IcerikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dosyalar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BenzerlikSonuclari",
                columns: table => new
                {
                    SonucId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlkDosyaId = table.Column<int>(type: "int", nullable: false),
                    IkinciDosyaId = table.Column<int>(type: "int", nullable: false),
                    BenzerlikOrani = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenzerlikSonuclari", x => x.SonucId);
                    table.ForeignKey(
                        name: "FK_BenzerlikSonuclari_Dosyalar_IkinciDosyaId",
                        column: x => x.IkinciDosyaId,
                        principalTable: "Dosyalar",
                        principalColumn: "DosyaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BenzerlikSonuclari_Dosyalar_IlkDosyaId",
                        column: x => x.IlkDosyaId,
                        principalTable: "Dosyalar",
                        principalColumn: "DosyaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenzerlikSonuclari_IkinciDosyaId",
                table: "BenzerlikSonuclari",
                column: "IkinciDosyaId");

            migrationBuilder.CreateIndex(
                name: "IX_BenzerlikSonuclari_IlkDosyaId",
                table: "BenzerlikSonuclari",
                column: "IlkDosyaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dosyalar_IcerikId",
                table: "Dosyalar",
                column: "IcerikId");

            migrationBuilder.CreateIndex(
                name: "IX_Dosyalar_KullaniciId",
                table: "Dosyalar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Icerikler_KullaniciId",
                table: "Icerikler",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenzerlikSonuclari");

            migrationBuilder.DropTable(
                name: "Dosyalar");

            migrationBuilder.DropTable(
                name: "Icerikler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
