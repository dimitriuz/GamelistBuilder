﻿// <auto-generated />
using System;
using GamelistBuilder.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GamelistBuilder.Migrations
{
    [DbContext(typeof(GamelistBuilderContext))]
    [Migration("20181018132459_second")]
    partial class second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GamelistBuilder.Models.FileExtensions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Extension");

                    b.Property<int?>("PlatformId");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("FileExtensions");
                });

            modelBuilder.Entity("GamelistBuilder.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desc");

                    b.Property<string>("Developer");

                    b.Property<bool>("Favorite");

                    b.Property<int?>("GameFolderId");

                    b.Property<int?>("GamelistId")
                        .IsRequired();

                    b.Property<string>("Genre");

                    b.Property<string>("Hash");

                    b.Property<string>("Image");

                    b.Property<bool>("ImageFound");

                    b.Property<bool>("IsFolder");

                    b.Property<bool>("MarqueFound");

                    b.Property<string>("Marquee");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<string>("Players");

                    b.Property<string>("Publisher");

                    b.Property<float>("Rating");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<bool>("RomFound");

                    b.Property<string>("Source");

                    b.Property<string>("SourceId");

                    b.Property<string>("Video");

                    b.Property<bool>("VideoFound");

                    b.HasKey("Id");

                    b.HasIndex("GameFolderId");

                    b.HasIndex("GamelistId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GamelistBuilder.Models.GameFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desc");

                    b.Property<int?>("GamelistId")
                        .IsRequired();

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<float>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("GamelistId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("GamelistBuilder.Models.Gamelist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("GamesDirectory")
                        .IsRequired();

                    b.Property<string>("ImagesDirectory");

                    b.Property<bool>("Imported");

                    b.Property<string>("MarqueDirectory");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<int?>("PlatformId");

                    b.Property<string>("VideoDirectory");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("Gamelists");
                });

            modelBuilder.Entity("GamelistBuilder.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GamelistBuilder.Models.FileExtensions", b =>
                {
                    b.HasOne("GamelistBuilder.Models.Platform")
                        .WithMany("Extensions")
                        .HasForeignKey("PlatformId");
                });

            modelBuilder.Entity("GamelistBuilder.Models.Game", b =>
                {
                    b.HasOne("GamelistBuilder.Models.GameFolder", "GameFolder")
                        .WithMany("Games")
                        .HasForeignKey("GameFolderId");

                    b.HasOne("GamelistBuilder.Models.Gamelist", "Gamelist")
                        .WithMany("Games")
                        .HasForeignKey("GamelistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GamelistBuilder.Models.GameFolder", b =>
                {
                    b.HasOne("GamelistBuilder.Models.Gamelist", "Gamelist")
                        .WithMany("GameFolders")
                        .HasForeignKey("GamelistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GamelistBuilder.Models.Gamelist", b =>
                {
                    b.HasOne("GamelistBuilder.Models.Platform", "Platform")
                        .WithMany("Gamelists")
                        .HasForeignKey("PlatformId");
                });
#pragma warning restore 612, 618
        }
    }
}
