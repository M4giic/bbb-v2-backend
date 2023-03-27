using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationUserService.Migrations
{
    public partial class AddAccoundGuidToSSTokenRepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountGuid",
                table: "_singleSignOnTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountGuid",
                table: "_singleSignOnTokens");
        }
    }
}
