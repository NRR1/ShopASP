using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopASP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Pathronomic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pathronomic",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pathronomic",
                table: "AspNetUsers");
        }
    }
}
