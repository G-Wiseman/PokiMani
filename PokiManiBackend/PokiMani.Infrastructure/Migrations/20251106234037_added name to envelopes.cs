using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokiMani.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addednametoenvelopes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Envelopes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Envelopes");
        }
    }
}
