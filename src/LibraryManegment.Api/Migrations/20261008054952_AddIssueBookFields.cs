using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManegment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueBookFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedDueDate",
                table: "IssueBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "IssueBooks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "IssueBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueBooks_RoleId",
                table: "IssueBooks",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBooks_Roles_RoleId",
                table: "IssueBooks",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueBooks_Roles_RoleId",
                table: "IssueBooks");

            migrationBuilder.DropIndex(
                name: "IX_IssueBooks_RoleId",
                table: "IssueBooks");

            migrationBuilder.DropColumn(
                name: "RequestedDueDate",
                table: "IssueBooks");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "IssueBooks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "IssueBooks");
        }
    }
}
