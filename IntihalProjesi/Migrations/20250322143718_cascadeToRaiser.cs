using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntihalProjesi.Migrations
{
    /// <inheritdoc />
    public partial class cascadeToRaiser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IkinciDosyaId",
                table: "BenzerlikSonuclari");

            migrationBuilder.DropForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IlkDosyaId",
                table: "BenzerlikSonuclari");

            migrationBuilder.DropForeignKey(
                name: "FK_Dosyalar_Kullanicilar_KullaniciId",
                table: "Dosyalar");

            migrationBuilder.AddForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IkinciDosyaId",
                table: "BenzerlikSonuclari",
                column: "IkinciDosyaId",
                principalTable: "Dosyalar",
                principalColumn: "DosyaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IlkDosyaId",
                table: "BenzerlikSonuclari",
                column: "IlkDosyaId",
                principalTable: "Dosyalar",
                principalColumn: "DosyaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dosyalar_Kullanicilar_KullaniciId",
                table: "Dosyalar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IkinciDosyaId",
                table: "BenzerlikSonuclari");

            migrationBuilder.DropForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IlkDosyaId",
                table: "BenzerlikSonuclari");

            migrationBuilder.DropForeignKey(
                name: "FK_Dosyalar_Kullanicilar_KullaniciId",
                table: "Dosyalar");

            migrationBuilder.AddForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IkinciDosyaId",
                table: "BenzerlikSonuclari",
                column: "IkinciDosyaId",
                principalTable: "Dosyalar",
                principalColumn: "DosyaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BenzerlikSonuclari_Dosyalar_IlkDosyaId",
                table: "BenzerlikSonuclari",
                column: "IlkDosyaId",
                principalTable: "Dosyalar",
                principalColumn: "DosyaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dosyalar_Kullanicilar_KullaniciId",
                table: "Dosyalar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
