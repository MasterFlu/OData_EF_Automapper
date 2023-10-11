using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    UId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StampEdit = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StampCreate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StampSync = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Oid = table.Column<int>(type: "INTEGER", nullable: true),
                    SeqNum = table.Column<int>(type: "INTEGER", nullable: true),
                    SortNum = table.Column<int>(type: "INTEGER", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.UId);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    UId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessUId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StampEdit = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StampCreate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StampSync = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Oid = table.Column<int>(type: "INTEGER", nullable: true),
                    SeqNum = table.Column<int>(type: "INTEGER", nullable: true),
                    SortNum = table.Column<int>(type: "INTEGER", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.UId);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Processes_ProcessUId",
                        column: x => x.ProcessUId,
                        principalTable: "Processes",
                        principalColumn: "UId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_ProcessUId",
                table: "TimeSheets",
                column: "ProcessUId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
