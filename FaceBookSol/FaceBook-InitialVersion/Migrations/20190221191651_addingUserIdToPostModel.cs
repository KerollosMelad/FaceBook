using Microsoft.EntityFrameworkCore.Migrations;

namespace FaceBook_InitialVersion.Migrations
{
    public partial class addingUserIdToPostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                newName: "IX_Posts_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
