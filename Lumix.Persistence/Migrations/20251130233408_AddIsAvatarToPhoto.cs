using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumix.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvatarToPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvatar",
                table: "Photos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvatar",
                table: "Photos");
        }
    }
}
