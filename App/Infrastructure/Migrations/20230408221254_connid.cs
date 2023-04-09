using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineHealthCheck.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class connid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "WorkQueue",
                newName: "ConnectionId");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "MachineInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "MachineInfo");

            migrationBuilder.RenameColumn(
                name: "ConnectionId",
                table: "WorkQueue",
                newName: "Key");
        }
    }
}
