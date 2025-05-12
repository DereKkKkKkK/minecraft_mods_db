using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreateAt_UpdateAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Mods",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Mods",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Collections",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeToComplete",
                table: "Collections",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "DifficultyId",
                table: "Collections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModLoaderId",
                table: "Collections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VersionId",
                table: "Collections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CollectionMod",
                columns: table => new
                {
                    CollectionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionMod", x => new { x.CollectionsId, x.ModsId });
                    table.ForeignKey(
                        name: "FK_CollectionMod_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionMod_Mods_ModsId",
                        column: x => x.ModsId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Focuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Focuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionFocus",
                columns: table => new
                {
                    CollectionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FocusesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionFocus", x => new { x.CollectionsId, x.FocusesId });
                    table.ForeignKey(
                        name: "FK_CollectionFocus_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionFocus_Focuses_FocusesId",
                        column: x => x.FocusesId,
                        principalTable: "Focuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_DifficultyId",
                table: "Collections",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_ModLoaderId",
                table: "Collections",
                column: "ModLoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_VersionId",
                table: "Collections",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionFocus_FocusesId",
                table: "CollectionFocus",
                column: "FocusesId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionMod_ModsId",
                table: "CollectionMod",
                column: "ModsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Difficulties_DifficultyId",
                table: "Collections",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_ModLoaders_ModLoaderId",
                table: "Collections",
                column: "ModLoaderId",
                principalTable: "ModLoaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_ModVersions_VersionId",
                table: "Collections",
                column: "VersionId",
                principalTable: "ModVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Difficulties_DifficultyId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_ModLoaders_ModLoaderId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_ModVersions_VersionId",
                table: "Collections");

            migrationBuilder.DropTable(
                name: "CollectionFocus");

            migrationBuilder.DropTable(
                name: "CollectionMod");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Focuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_DifficultyId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_ModLoaderId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_VersionId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "DifficultyId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ModLoaderId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "Collections");

            migrationBuilder.AlterColumn<int>(
                name: "TimeToComplete",
                table: "Collections",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Collections",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "TimeToComplete");
        }
    }
}
