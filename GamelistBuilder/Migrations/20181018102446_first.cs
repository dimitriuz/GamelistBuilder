using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamelistBuilder.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Extension = table.Column<string>(nullable: true),
                    PlatformId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileExtensions_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gamelists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: false),
                    GamesDirectory = table.Column<string>(nullable: false),
                    ImagesDirectory = table.Column<string>(nullable: true),
                    VideoDirectory = table.Column<string>(nullable: true),
                    MarqueDirectory = table.Column<string>(nullable: true),
                    PlatformId = table.Column<int>(nullable: true),
                    Imported = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamelists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamelists_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<float>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    GamelistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folders_Gamelists_GamelistId",
                        column: x => x.GamelistId,
                        principalTable: "Gamelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceId = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Developer = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Players = table.Column<string>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Marquee = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true),
                    Favorite = table.Column<bool>(nullable: false),
                    IsFolder = table.Column<bool>(nullable: false),
                    RomFound = table.Column<bool>(nullable: false),
                    ImageFound = table.Column<bool>(nullable: false),
                    VideoFound = table.Column<bool>(nullable: false),
                    MarqueFound = table.Column<bool>(nullable: false),
                    GamelistId = table.Column<int>(nullable: true),
                    GameFolderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Folders_GameFolderId",
                        column: x => x.GameFolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Gamelists_GamelistId",
                        column: x => x.GamelistId,
                        principalTable: "Gamelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileExtensions_PlatformId",
                table: "FileExtensions",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_GamelistId",
                table: "Folders",
                column: "GamelistId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamelists_PlatformId",
                table: "Gamelists",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameFolderId",
                table: "Games",
                column: "GameFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GamelistId",
                table: "Games",
                column: "GamelistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileExtensions");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropTable(
                name: "Gamelists");

            migrationBuilder.DropTable(
                name: "Platforms");
        }
    }
}
