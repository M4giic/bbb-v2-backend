using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OperationService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_walletEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerUserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__walletEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateWhenHappened = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubType = table.Column<int>(type: "int", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationEntity__walletEntities_WalletId",
                        column: x => x.WalletId,
                        principalTable: "_walletEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlannedOperationEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhenHappens = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysItRepeats = table.Column<int>(type: "int", nullable: false),
                    LastHappend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubType = table.Column<int>(type: "int", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedOperationEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedOperationEntity__walletEntities_WalletId",
                        column: x => x.WalletId,
                        principalTable: "_walletEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationEntity_WalletId",
                table: "OperationEntity",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedOperationEntity_WalletId",
                table: "PlannedOperationEntity",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationEntity");

            migrationBuilder.DropTable(
                name: "PlannedOperationEntity");

            migrationBuilder.DropTable(
                name: "_walletEntities");
        }
    }
}
