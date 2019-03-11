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
                name: "goals",
                schema: "goal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(maxLength: 200, nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateDue = table.Column<DateTime>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    GoalSettings__goalViewAccessibilityId = table.Column<int>(nullable: false),
                    GoalSettings__goalCommentAccessibilityId = table.Column<int>(nullable: false),
                    GoalSettings_GoalCommentAccessibilityId = table.Column<int>(nullable: true),
                    GoalSettings_GoalViewAccessibilityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goals", x => x.Id);
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
                name: "GoalDependency",
                columns: table => new
                {
                    GoalId = table.Column<int>(nullable: false),
                    DependentOnGoalId = table.Column<int>(nullable: false),
                    GoalId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalDependency", x => new { x.GoalId, x.DependentOnGoalId });
                    table.ForeignKey(
                        name: "FK_GoalDependency_goals_DependentOnGoalId",
                        column: x => x.DependentOnGoalId,
                        principalSchema: "goal",
                        principalTable: "goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalDependency_goals_GoalId1",
                        column: x => x.GoalId1,
                        principalSchema: "goal",
                        principalTable: "goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalDependency_DependentOnGoalId",
                table: "GoalDependency",
                column: "DependentOnGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalDependency_GoalId1",
                table: "GoalDependency",
                column: "GoalId1");

            migrationBuilder.CreateIndex(
                name: "IX_GoalDependency_GoalId_DependentOnGoalId",
                table: "GoalDependency",
                columns: new[] { "GoalId", "DependentOnGoalId" });

            migrationBuilder.CreateIndex(
                name: "IX_goals_UserId",
                schema: "goal",
                table: "goals",
                column: "UserId");

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
                name: "GoalDependency");

            migrationBuilder.DropTable(
                name: "goals",
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
