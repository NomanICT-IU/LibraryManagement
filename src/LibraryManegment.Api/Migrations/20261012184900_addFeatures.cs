using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManegment.Api.Migrations
{
    /// <inheritdoc />
    public partial class addFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "IssueBooks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "IssueBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueBooks_BookId1",
                table: "IssueBooks",
                column: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_IssueBooks_UserId1",
                table: "IssueBooks",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBooks_Books_BookId1",
                table: "IssueBooks",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueBooks_Users_UserId1",
                table: "IssueBooks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueBooks_Books_BookId1",
                table: "IssueBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueBooks_Users_UserId1",
                table: "IssueBooks");

            migrationBuilder.DropIndex(
                name: "IX_IssueBooks_BookId1",
                table: "IssueBooks");

            migrationBuilder.DropIndex(
                name: "IX_IssueBooks_UserId1",
                table: "IssueBooks");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "IssueBooks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "IssueBooks");
        }
    }
}
