using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManegment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueBook_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueBook_BookId_UserId",
                table: "IssueBook",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_IssueBook_UserId",
                table: "IssueBook",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueBook");
        }
    }
}
