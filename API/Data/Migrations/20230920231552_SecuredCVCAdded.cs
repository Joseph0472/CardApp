using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecuredCVCAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVC",
                table: "Cards");

            migrationBuilder.AddColumn<byte[]>(
                name: "CVCHash",
                table: "Cards",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CVCSalt",
                table: "Cards",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVCHash",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CVCSalt",
                table: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "CVC",
                table: "Cards",
                type: "TEXT",
                nullable: true);
        }
    }
}
