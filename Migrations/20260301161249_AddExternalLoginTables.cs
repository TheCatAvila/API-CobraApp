using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_CobraApp.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalLoginTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProviderName = table.Column<string>(type: "TEXT", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserExternals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExternalProviderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProviderUserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExternals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExternals_ExternalProviders_ExternalProviderId",
                        column: x => x.ExternalProviderId,
                        principalTable: "ExternalProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserExternals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalProviders_ProviderName",
                table: "ExternalProviders",
                column: "ProviderName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserExternals_ExternalProviderId",
                table: "UserExternals",
                column: "ExternalProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExternals_UserId",
                table: "UserExternals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExternals");

            migrationBuilder.DropTable(
                name: "ExternalProviders");
        }
    }
}
