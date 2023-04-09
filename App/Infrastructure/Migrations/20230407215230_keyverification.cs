using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineHealthCheck.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class keyverification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
                table: "MachineInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "MachineInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "MachineInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "MachineInfo");

            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "MachineInfo");

            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
                table: "MachineInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
