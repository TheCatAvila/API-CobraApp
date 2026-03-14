using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_CobraApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedExternalProviders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExternalProviders",
                columns: new[] { "Id", "IsEnabled", "ProviderName" },
                values: new object[] { 1, true, "Google" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExternalProviders",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
