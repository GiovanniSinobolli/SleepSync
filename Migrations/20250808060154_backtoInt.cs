using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepSync.Migrations
{
    /// <inheritdoc />
    public partial class backtoInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sleepRecords_AspNetUsers_UserId",
                table: "sleepRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_AspNetUsers_UserId",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTasks_UserId",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sleepRecords",
                table: "sleepRecords");

            migrationBuilder.RenameTable(
                name: "sleepRecords",
                newName: "SleepRecords");

            migrationBuilder.RenameIndex(
                name: "IX_sleepRecords_UserId",
                table: "SleepRecords",
                newName: "IX_SleepRecords_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SleepRecords",
                table: "SleepRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SleepRecords_AspNetUsers_UserId",
                table: "SleepRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SleepRecords_AspNetUsers_UserId",
                table: "SleepRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SleepRecords",
                table: "SleepRecords");

            migrationBuilder.RenameTable(
                name: "SleepRecords",
                newName: "sleepRecords");

            migrationBuilder.RenameIndex(
                name: "IX_SleepRecords_UserId",
                table: "sleepRecords",
                newName: "IX_sleepRecords_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserTasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sleepRecords",
                table: "sleepRecords",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_UserId",
                table: "UserTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_sleepRecords_AspNetUsers_UserId",
                table: "sleepRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_AspNetUsers_UserId",
                table: "UserTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
