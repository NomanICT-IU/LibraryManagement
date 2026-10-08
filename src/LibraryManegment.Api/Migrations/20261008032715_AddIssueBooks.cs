using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManegment.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueBook_Books_BookId",
                table: "IssueBook");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueBook_Users_UserId",
                table: "IssueBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueBook",
                table: "IssueBook");

            migrationBuilder.RenameTable(
                name: "IssueBook",
                newName: "IssueBooks");

            migrationBuilder.RenameIndex(
                name: "IX_IssueBook_UserId",
                table: "IssueBooks",
                newName: "IX_IssueBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueBook_BookId_UserId",
                table: "IssueBooks",
                newName: "IX_IssueBooks_BookId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueBooks",
                table: "IssueBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBooks_Books_BookId",
                table: "IssueBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBooks_Users_UserId",
                table: "IssueBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueBooks_Books_BookId",
                table: "IssueBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueBooks_Users_UserId",
                table: "IssueBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueBooks",
                table: "IssueBooks");

            migrationBuilder.RenameTable(
                name: "IssueBooks",
                newName: "IssueBook");

            migrationBuilder.RenameIndex(
                name: "IX_IssueBooks_UserId",
                table: "IssueBook",
                newName: "IX_IssueBook_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueBooks_BookId_UserId",
                table: "IssueBook",
                newName: "IX_IssueBook_BookId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueBook",
                table: "IssueBook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBook_Books_BookId",
                table: "IssueBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBook_Users_UserId",
                table: "IssueBook",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
