using Microsoft.EntityFrameworkCore.Migrations;

namespace FaceBook_InitialVersion.Migrations
{
    public partial class fourthM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_PersonId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PersonId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID");

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

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserID",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PersonId",
                table: "Posts",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_PersonId",
                table: "Posts",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
