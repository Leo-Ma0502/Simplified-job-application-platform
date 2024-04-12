using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class secondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UId");

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    IId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.IId);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opening = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsibilities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JId);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    KId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.KId);
                });

            migrationBuilder.CreateTable(
                name: "JobIndustries",
                columns: table => new
                {
                    JId = table.Column<int>(type: "int", nullable: false),
                    IId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobIndustries", x => new { x.JId, x.IId });
                    table.ForeignKey(
                        name: "FK_JobIndustries_Industries_IId",
                        column: x => x.IId,
                        principalTable: "Industries",
                        principalColumn: "IId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobIndustries_Jobs_JId",
                        column: x => x.JId,
                        principalTable: "Jobs",
                        principalColumn: "JId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobKeywords",
                columns: table => new
                {
                    JId = table.Column<int>(type: "int", nullable: false),
                    KId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobKeywords", x => new { x.JId, x.KId });
                    table.ForeignKey(
                        name: "FK_JobKeywords_Jobs_JId",
                        column: x => x.JId,
                        principalTable: "Jobs",
                        principalColumn: "JId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobKeywords_Keywords_KId",
                        column: x => x.KId,
                        principalTable: "Keywords",
                        principalColumn: "KId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobIndustries_IId",
                table: "JobIndustries",
                column: "IId");

            migrationBuilder.CreateIndex(
                name: "IX_JobKeywords_KId",
                table: "JobKeywords",
                column: "KId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobIndustries");

            migrationBuilder.DropTable(
                name: "JobKeywords");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.RenameColumn(
                name: "UId",
                table: "Users",
                newName: "Id");
        }
    }
}
