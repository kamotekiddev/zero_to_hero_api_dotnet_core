using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeroToHeroAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyQuestStatusfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "QuestRewards");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "QuestPunishments");

            migrationBuilder.RenameColumn(
                name: "MinValue",
                table: "QuestRewards",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "MinValue",
                table: "QuestPunishments",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "QuestStatus",
                table: "DailyQuests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestStatus",
                table: "DailyQuests");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "QuestRewards",
                newName: "MinValue");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "QuestPunishments",
                newName: "MinValue");

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "QuestRewards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "QuestPunishments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
