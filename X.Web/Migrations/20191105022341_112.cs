using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace X.Web.Migrations
{
    public partial class _112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TypeName = table.Column<string>(nullable: false),
                    AuditEnabled = table.Column<bool>(nullable: false),
                    PropertyJson = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "ClassFullNameIndex",
                table: "EntityInfo",
                column: "TypeName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityInfo");
        }
    }
}
