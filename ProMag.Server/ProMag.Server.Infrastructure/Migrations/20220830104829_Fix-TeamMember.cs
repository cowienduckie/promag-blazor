using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProMag.Server.Infrastructure.Migrations
{
    public partial class FixTeamMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMember_TeamMember_TeamMemberId",
                schema: "dbo",
                table: "TeamMember");

            migrationBuilder.DropIndex(
                name: "IX_TeamMember_TeamMemberId",
                schema: "dbo",
                table: "TeamMember");

            migrationBuilder.DropColumn(
                name: "TeamMemberId",
                schema: "dbo",
                table: "TeamMember");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamMemberId",
                schema: "dbo",
                table: "TeamMember",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamMemberId",
                schema: "dbo",
                table: "TeamMember",
                column: "TeamMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMember_TeamMember_TeamMemberId",
                schema: "dbo",
                table: "TeamMember",
                column: "TeamMemberId",
                principalSchema: "dbo",
                principalTable: "TeamMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
