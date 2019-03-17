using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasks.Infrastructure.Migrations
{
    public partial class AddedCommenting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings__goalCommentAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings__goalViewAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropIndex(
                name: "IX_goals_GoalSettings__goalCommentAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropIndex(
                name: "IX_goals_GoalSettings__goalViewAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropColumn(
                name: "GoalSettings__goalCommentAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropColumn(
                name: "GoalSettings__goalViewAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.AlterColumn<int>(
                name: "GoalSettings_GoalViewAccessibilityId",
                schema: "goal",
                table: "goals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GoalSettings_GoalCommentAccessibilityId",
                schema: "goal",
                table: "goals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "diary_posts",
                schema: "goal",
                columns: table => new
                {
                    IdentityGuid = table.Column<int>(maxLength: 64, nullable: false),
                    DiaryPostId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    PostedTime = table.Column<DateTime>(nullable: false),
                    GoalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diary_posts", x => new { x.IdentityGuid, x.DiaryPostId, x.Content });
                    table.ForeignKey(
                        name: "FK_diary_posts_goals_GoalId",
                        column: x => x.GoalId,
                        principalSchema: "goal",
                        principalTable: "goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                schema: "goal",
                columns: table => new
                {
                    IdentityGuid = table.Column<int>(nullable: false),
                    DiaryPostId = table.Column<int>(nullable: false),
                    PostedTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    DiaryPostContent = table.Column<string>(nullable: false),
                    DiaryPostId1 = table.Column<int>(nullable: false),
                    DiaryPostIdentityGuid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => new { x.IdentityGuid, x.DiaryPostId });
                    table.ForeignKey(
                        name: "FK_comments_diary_posts_DiaryPostIdentityGuid_DiaryPostId1_DiaryPostContent",
                        columns: x => new { x.DiaryPostIdentityGuid, x.DiaryPostId1, x.DiaryPostContent },
                        principalSchema: "goal",
                        principalTable: "diary_posts",
                        principalColumns: new[] { "IdentityGuid", "DiaryPostId", "Content" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_goals_GoalSettings_GoalCommentAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings_GoalCommentAccessibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_goals_GoalSettings_GoalViewAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings_GoalViewAccessibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_DiaryPostIdentityGuid_DiaryPostId1_DiaryPostContent",
                schema: "goal",
                table: "comments",
                columns: new[] { "DiaryPostIdentityGuid", "DiaryPostId1", "DiaryPostContent" });

            migrationBuilder.CreateIndex(
                name: "IX_diary_posts_GoalId",
                schema: "goal",
                table: "diary_posts",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings_GoalCommentAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings_GoalCommentAccessibilityId",
                principalSchema: "goal",
                principalTable: "accessibility_modifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings_GoalViewAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings_GoalViewAccessibilityId",
                principalSchema: "goal",
                principalTable: "accessibility_modifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings_GoalCommentAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings_GoalViewAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropTable(
                name: "comments",
                schema: "goal");

            migrationBuilder.DropTable(
                name: "diary_posts",
                schema: "goal");

            migrationBuilder.DropIndex(
                name: "IX_goals_GoalSettings_GoalCommentAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.DropIndex(
                name: "IX_goals_GoalSettings_GoalViewAccessibilityId",
                schema: "goal",
                table: "goals");

            migrationBuilder.AlterColumn<int>(
                name: "GoalSettings_GoalViewAccessibilityId",
                schema: "goal",
                table: "goals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GoalSettings_GoalCommentAccessibilityId",
                schema: "goal",
                table: "goals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GoalSettings__goalCommentAccessibilityId",
                schema: "goal",
                table: "goals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalSettings__goalViewAccessibilityId",
                schema: "goal",
                table: "goals",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings__goalCommentAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings__goalCommentAccessibilityId",
                principalSchema: "goal",
                principalTable: "accessibility_modifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_goals_accessibility_modifiers_GoalSettings__goalViewAccessibilityId",
                schema: "goal",
                table: "goals",
                column: "GoalSettings__goalViewAccessibilityId",
                principalSchema: "goal",
                principalTable: "accessibility_modifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
