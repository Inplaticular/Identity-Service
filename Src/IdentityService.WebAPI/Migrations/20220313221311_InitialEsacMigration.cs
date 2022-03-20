using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inplanticular.IdentityService.WebAPI.Migrations
{
    public partial class InitialEsacMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationalGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalUnits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    GroupId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalUnits_OrganizationalGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "OrganizationalGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalUnitUserClaims",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalUnitUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalUnitUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationalUnitUserClaims_OrganizationalUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "OrganizationalUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalGroups_Name",
                table: "OrganizationalGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalUnits_GroupId",
                table: "OrganizationalUnits",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalUnits_Name",
                table: "OrganizationalUnits",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalUnitUserClaims_UnitId_UserId_Type_Value",
                table: "OrganizationalUnitUserClaims",
                columns: new[] { "UnitId", "UserId", "Type", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalUnitUserClaims_UserId",
                table: "OrganizationalUnitUserClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationalUnitUserClaims");

            migrationBuilder.DropTable(
                name: "OrganizationalUnits");

            migrationBuilder.DropTable(
                name: "OrganizationalGroups");
        }
    }
}
