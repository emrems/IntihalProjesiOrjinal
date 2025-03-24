using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntihalProjesi.Migrations
{
    /// <inheritdoc />
    public partial class adminEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kullanicilar",
                columns: new[] { "KullaniciId", "Ad", "Eposta", "Rol", "Sifre", "Soyad" },
                values: new object[] { 1, "emre", "emre@gmail.com", "Admin", "emre123", "almamış" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kullanicilar",
                keyColumn: "KullaniciId",
                keyValue: 1);
        }
    }
}
