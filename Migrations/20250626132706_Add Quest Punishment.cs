using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeroToHeroAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestPunishment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestPunishments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    QuestTemplateId = table.Column<string>(type: "text", nullable: false),
                    PunishmentType = table.Column<string>(type: "text", nullable: false),
                    MinValue = table.Column<int>(type: "integer", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestPunishments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestPunishments_QuestTemplates_QuestTemplateId",
                        column: x => x.QuestTemplateId,
                        principalTable: "QuestTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestPunishments_QuestTemplateId",
                table: "QuestPunishments",
                column: "QuestTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestPunishments");
        }
    }
}
