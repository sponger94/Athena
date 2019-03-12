using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Goals.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "goal");

            migrationBuilder.CreateSequence(
                name: "goalseq",
                schema: "goal",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "accessibility_modifiers",
                schema: "goal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessibility_modifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "goal_statuses",
                schema: "goal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "goals",
                schema: "goal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IdentityGuid = table.Column<string>(maxLength: 200, nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateDue = table.Column<DateTime>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    GoalSettings__goalViewAccessibilityId = table.Column<int>(nullable: false),
                    GoalSettings__goalCommentAccessibilityId = table.Column<int>(nullable: false),
                    GoalSettings_GoalCommentAccessibilityId = table.Column<int>(nullable: true),
                    GoalSettings_GoalViewAccessibilityId = table.Column<int>(nullable: true),
                    GoalStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goals_goal_statuses_GoalStatusId",
                        column: x => x.GoalStatusId,
                        principalSchema: "goal",
                        principalTable: "goal_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_goals_accessibility_modifiers_GoalSettings__goalCommentAccessibilityId",
                        column: x => x.GoalSettings__goalCommentAccessibilityId,
                        principalSchema: "goal",
                        principalTable: "accessibility_modifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goals_accessibility_modifiers_GoalSettings__goalViewAccessibilityId",
                        column: x => x.GoalSettings__goalViewAccessibilityId,
                        principalSchema: "goal",
                        principalTable: "accessibility_modifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalStep",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    GoalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalStep", x => new { x.GoalId, x.Name, x.Description, x.DueDate });
                    table.ForeignKey(
                        name: "FK_GoalStep_goals_GoalId",
                        column: x => x.GoalId,
                        principalSchema: "goal",
                        principalTable: "goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_goals_GoalStatusId",
                schema: "goal",
                table: "goals",
                column: "GoalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_goals_IdentityGuid",
                schema: "goal",
                table: "goals",
                column: "IdentityGuid");

            migrationBuilder.CreateIndex(
                name: "IX_goals_GoalSettings__goalCommentAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings__goalCommentAccessibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_goals_GoalSettings__goalViewAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings__goalViewAccessibilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalStep");

            migrationBuilder.DropTable(
                name: "goals",
                schema: "goal");

            migrationBuilder.DropTable(
                name: "goal_statuses",
                schema: "goal");

            migrationBuilder.DropTable(
                name: "accessibility_modifiers",
                schema: "goal");

            migrationBuilder.DropSequence(
                name: "goalseq",
                schema: "goal");
        }
    }
}
