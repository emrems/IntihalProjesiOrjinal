using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntihalProjesi.Migrations
{
    /// <inheritdoc />
    public partial class ilkMigrattion : Migration
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
                    IcerikTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Bildirimler",
                columns: table => new
                {
                    BildirimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mesaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Okundu = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    IcerikId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bildirimler", x => x.BildirimId);
                    table.ForeignKey(
                        name: "FK_Bildirimler_Icerikler_IcerikId",
                        column: x => x.IcerikId,
                        principalTable: "Icerikler",
                        principalColumn: "IcerikId");
                    table.ForeignKey(
                        name: "FK_Bildirimler_Kullanicilar_KullaniciId",
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
                    KullaniciId = table.Column<int>(type: "int", nullable: true),
                    IcerikId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosyalar", x => x.DosyaId);
                    table.ForeignKey(
                        name: "FK_Dosyalar_Icerikler_IcerikId",
                        column: x => x.IcerikId,
                        principalTable: "Icerikler",
                        principalColumn: "IcerikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dosyalar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId",
                        onDelete: ReferentialAction.SetNull);
                });

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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BenzerlikSonuclari",
                columns: table => new
                {
                    SonucId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlkDosyaId = table.Column<int>(type: "int", nullable: true),
                    IkinciDosyaId = table.Column<int>(type: "int", nullable: true),
                    IcerikId = table.Column<int>(type: "int", nullable: true),
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
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BenzerlikSonuclari_Icerikler_IcerikId",
                        column: x => x.IcerikId,
                        principalTable: "Icerikler",
                        principalColumn: "IcerikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Kullanicilar",
                columns: new[] { "KullaniciId", "Ad", "Eposta", "Rol", "Sifre", "Soyad" },
                values: new object[] { 1, "emre", "emre@gmail.com", "Admin", "emre123", "almamış" });

            migrationBuilder.CreateIndex(
                name: "IX_BenzerlikSonuclari_IcerikId",
                table: "BenzerlikSonuclari",
                column: "IcerikId");

            migrationBuilder.CreateIndex(
                name: "IX_BenzerlikSonuclari_IkinciDosyaId",
                table: "BenzerlikSonuclari",
                column: "IkinciDosyaId");

            migrationBuilder.CreateIndex(
                name: "IX_BenzerlikSonuclari_IlkDosyaId",
                table: "BenzerlikSonuclari",
                column: "IlkDosyaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_IcerikId",
                table: "Bildirimler",
                column: "IcerikId");

            migrationBuilder.CreateIndex(
                name: "IX_Bildirimler_KullaniciId",
                table: "Bildirimler",
                column: "KullaniciId");

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

            migrationBuilder.CreateIndex(
                name: "IX_JplagJobs_IcerikId",
                table: "JplagJobs",
                column: "IcerikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenzerlikSonuclari");

            migrationBuilder.DropTable(
                name: "Bildirimler");

            migrationBuilder.DropTable(
                name: "JplagJobs");

            migrationBuilder.DropTable(
                name: "Dosyalar");

            migrationBuilder.DropTable(
                name: "Icerikler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
