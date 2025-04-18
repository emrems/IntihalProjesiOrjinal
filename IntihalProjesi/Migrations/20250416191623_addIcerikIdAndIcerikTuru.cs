using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntihalProjesi.Migrations
{
    /// <inheritdoc />
    public partial class addIcerikIdAndIcerikTuru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IcerikTuru",
                table: "Icerikler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "IlkDosyaId",
                table: "BenzerlikSonuclari",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IkinciDosyaId",
                table: "BenzerlikSonuclari",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IcerikId",
                table: "BenzerlikSonuclari",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BenzerlikSonuclari_IcerikId",
                table: "BenzerlikSonuclari",
                column: "IcerikId");

            migrationBuilder.AddForeignKey(
                name: "FK_BenzerlikSonuclari_Icerikler_IcerikId",
                table: "BenzerlikSonuclari",
                column: "IcerikId",
                principalTable: "Icerikler",
                principalColumn: "IcerikId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenzerlikSonuclari_Icerikler_IcerikId",
                table: "BenzerlikSonuclari");

            migrationBuilder.DropIndex(
                name: "IX_BenzerlikSonuclari_IcerikId",
                table: "BenzerlikSonuclari");

            migrationBuilder.DropColumn(
                name: "IcerikTuru",
                table: "Icerikler");

            migrationBuilder.DropColumn(
                name: "IcerikId",
                table: "BenzerlikSonuclari");

            migrationBuilder.AlterColumn<int>(
                name: "IlkDosyaId",
                table: "BenzerlikSonuclari",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IkinciDosyaId",
                table: "BenzerlikSonuclari",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
