using Microsoft.EntityFrameworkCore.Migrations;

namespace GamelistBuilder.Migrations
{
    public partial class five : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GamelistId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games",
                column: "GamelistId",
                principalTable: "Gamelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GamelistId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games",
                column: "GamelistId",
                principalTable: "Gamelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
