using Microsoft.EntityFrameworkCore.Migrations;

namespace GamelistBuilder.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Gamelists_GamelistId",
                table: "Folders");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Gamelists_GamelistId",
                table: "Folders");

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
        }
    }
}
