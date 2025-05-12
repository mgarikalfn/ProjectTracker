using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedProjectIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Projects");
        }
    }
}
