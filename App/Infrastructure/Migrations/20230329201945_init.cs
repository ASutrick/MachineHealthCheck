using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineHealthCheck.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthCheck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MachineInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OSVersion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCheck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthCheck_MachineInfo_MachineInfoId",
                        column: x => x.MachineInfoId,
                        principalTable: "MachineInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HealthCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfCores = table.Column<int>(type: "int", nullable: false),
                    NumOfLogicalProcessors = table.Column<int>(type: "int", nullable: false),
                    CurrClockSpeed = table.Column<int>(type: "int", nullable: false),
                    PercentInUse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUInfo_HealthCheck_HealthCheckId",
                        column: x => x.HealthCheckId,
                        principalTable: "HealthCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiskInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HealthCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CapacityMb = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FreeSpaceMb = table.Column<long>(type: "bigint", nullable: false),
                    PercentUtilization = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiskInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiskInfo_HealthCheck_HealthCheckId",
                        column: x => x.HealthCheckId,
                        principalTable: "HealthCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoryInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HealthCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPhysicalMb = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PercentInUse = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoryInfo_HealthCheck_HealthCheckId",
                        column: x => x.HealthCheckId,
                        principalTable: "HealthCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SqlInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HealthCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasSqlServer = table.Column<bool>(type: "bit", nullable: false),
                    SqlServerVersion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SqlInfo_HealthCheck_HealthCheckId",
                        column: x => x.HealthCheckId,
                        principalTable: "HealthCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CPUInfo_HealthCheckId",
                table: "CPUInfo",
                column: "HealthCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_DiskInfo_HealthCheckId",
                table: "DiskInfo",
                column: "HealthCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCheck_MachineInfoId",
                table: "HealthCheck",
                column: "MachineInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryInfo_HealthCheckId",
                table: "MemoryInfo",
                column: "HealthCheckId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SqlInfo_HealthCheckId",
                table: "SqlInfo",
                column: "HealthCheckId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPUInfo");

            migrationBuilder.DropTable(
                name: "DiskInfo");

            migrationBuilder.DropTable(
                name: "MemoryInfo");

            migrationBuilder.DropTable(
                name: "SqlInfo");

            migrationBuilder.DropTable(
                name: "HealthCheck");

            migrationBuilder.DropTable(
                name: "MachineInfo");
        }
    }
}
