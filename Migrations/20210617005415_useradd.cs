using Microsoft.EntityFrameworkCore.Migrations;

namespace LocadoraDeFilmes.Migrations
{
    public partial class useradd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "locacao",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_locacao_userId",
                table: "locacao",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_locacao_AspNetUsers_userId",
                table: "locacao",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locacao_AspNetUsers_userId",
                table: "locacao");

            migrationBuilder.DropIndex(
                name: "IX_locacao_userId",
                table: "locacao");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "locacao");
        }
    }
}
