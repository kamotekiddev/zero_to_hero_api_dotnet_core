using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeroToHeroAPI.Migrations
{
    /// <inheritdoc />
    public partial class QuestManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyQuests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    QuestTemplateId = table.Column<string>(type: "text", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyQuests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyQuests_QuestTemplates_QuestTemplateId",
                        column: x => x.QuestTemplateId,
                        principalTable: "QuestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    QuestTemplateId = table.Column<string>(type: "text", nullable: false),
                    ActionType = table.Column<string>(type: "text", nullable: false),
                    TargetValue = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestActions_QuestTemplates_QuestTemplateId",
                        column: x => x.QuestTemplateId,
                        principalTable: "QuestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestRewards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    QuestTemplateId = table.Column<string>(type: "text", nullable: false),
                    RewardType = table.Column<string>(type: "text", nullable: false),
                    MinValue = table.Column<int>(type: "integer", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestRewards_QuestTemplates_QuestTemplateId",
                        column: x => x.QuestTemplateId,
                        principalTable: "QuestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestActionProgresses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DailyQuestId = table.Column<string>(type: "text", nullable: false),
                    QuestActionId = table.Column<string>(type: "text", nullable: false),
                    ProgressValue = table.Column<int>(type: "integer", nullable: false),
                    IsActionCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestActionProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestActionProgresses_DailyQuests_DailyQuestId",
                        column: x => x.DailyQuestId,
                        principalTable: "DailyQuests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestActionProgresses_QuestActions_QuestActionId",
                        column: x => x.QuestActionId,
                        principalTable: "QuestActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyQuests_QuestTemplateId",
                table: "DailyQuests",
                column: "QuestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestActionProgresses_DailyQuestId",
                table: "QuestActionProgresses",
                column: "DailyQuestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestActionProgresses_QuestActionId",
                table: "QuestActionProgresses",
                column: "QuestActionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestActions_QuestTemplateId",
                table: "QuestActions",
                column: "QuestTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestRewards_QuestTemplateId",
                table: "QuestRewards",
                column: "QuestTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestActionProgresses");

            migrationBuilder.DropTable(
                name: "QuestRewards");

            migrationBuilder.DropTable(
                name: "DailyQuests");

            migrationBuilder.DropTable(
                name: "QuestActions");

            migrationBuilder.DropTable(
                name: "QuestTemplates");
        }
    }
}
