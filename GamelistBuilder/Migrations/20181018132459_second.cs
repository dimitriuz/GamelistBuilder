using Microsoft.EntityFrameworkCore.Migrations;

namespace GamelistBuilder.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Gamelists_GamelistId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GamelistId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GamelistId",
                table: "Folders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Gamelists_GamelistId",
                table: "Folders",
                column: "GamelistId",
                principalTable: "Gamelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games",
                column: "GamelistId",
                principalTable: "Gamelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Gamelists_GamelistId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GamelistId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GamelistId",
                table: "Folders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Gamelists_GamelistId",
                table: "Folders",
                column: "GamelistId",
                principalTable: "Gamelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Gamelists_GamelistId",
                table: "Games",
                column: "GamelistId",
                principalTable: "Gamelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
