using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProMag.Server.Infrastructure.Migrations
{
    public partial class SetupTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<bool>(
                name: "IsProjectManager",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "dbo",
                columns: table => new
                {
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                schema: "dbo",
                columns: table => new
                {
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamRole",
                schema: "dbo",
                columns: table => new
                {
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamMember",
                schema: "dbo",
                columns: table => new
                {
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TeamRoleId = table.Column<int>(type: "int", nullable: false),
                    TeamMemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMember_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "dbo",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamMember_Team_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "dbo",
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamMember_TeamMember_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalSchema: "dbo",
                        principalTable: "TeamMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamMember_TeamRole_TeamRoleId",
                        column: x => x.TeamRoleId,
                        principalSchema: "dbo",
                        principalTable: "TeamRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                schema: "dbo",
                table: "Employee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_EmployeeId",
                schema: "dbo",
                table: "TeamMember",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamId",
                schema: "dbo",
                table: "TeamMember",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamMemberId",
                schema: "dbo",
                table: "TeamMember",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamRoleId",
                schema: "dbo",
                table: "TeamMember",
                column: "TeamRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMember",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Team",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TeamRole",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "IsProjectManager",
                table: "Users");
        }
    }
}
