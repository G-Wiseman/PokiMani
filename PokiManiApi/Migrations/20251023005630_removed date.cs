using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokiMani.Migrations
{
    /// <inheritdoc />
    public partial class removeddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateJoined",
                table: "Users",
                type: "integer",
                nullable: true);
        }
    }
}
