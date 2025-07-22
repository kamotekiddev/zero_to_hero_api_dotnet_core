using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeroToHeroAPI.Migrations
{
    /// <inheritdoc />
    public partial class RelateDailyquesttoplayerthanaUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DailyQuests",
                newName: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyQuests_PlayerId",
                table: "DailyQuests",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyQuests_Player_PlayerId",
                table: "DailyQuests",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyQuests_Player_PlayerId",
                table: "DailyQuests");

            migrationBuilder.DropIndex(
                name: "IX_DailyQuests_PlayerId",
                table: "DailyQuests");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "DailyQuests",
                newName: "UserId");
        }
    }
}
