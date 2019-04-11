using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasks.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tasks");

            migrationBuilder.CreateSequence(
                name: "labelseq",
                schema: "tasks",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "projectsseq",
                schema: "tasks",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "taskseq",
                schema: "tasks",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "labels",
                schema: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Argb = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                schema: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IdentityGuid = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Argb = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userTasks",
                schema: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                schema: "tasks",
                columns: table => new
                {
                    Uri = table.Column<string>(nullable: false),
                    UserTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => new { x.UserTaskId, x.Uri });
                    table.ForeignKey(
                        name: "FK_attachments_userTasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalSchema: "tasks",
                        principalTable: "userTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "labelItems",
                schema: "tasks",
                columns: table => new
                {
                    LabelId = table.Column<int>(nullable: false),
                    UserTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labelItems", x => new { x.UserTaskId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_labelItems_labels_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "tasks",
                        principalTable: "labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_labelItems_userTasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalSchema: "tasks",
                        principalTable: "userTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                schema: "tasks",
                columns: table => new
                {
                    Content = table.Column<string>(nullable: false),
                    UserTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => new { x.UserTaskId, x.Content });
                    table.ForeignKey(
                        name: "FK_notes_userTasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalSchema: "tasks",
                        principalTable: "userTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subTasks",
                schema: "tasks",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    UserTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subTasks", x => new { x.UserTaskId, x.Name, x.IsCompleted });
                    table.ForeignKey(
                        name: "FK_subTasks_userTasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalSchema: "tasks",
                        principalTable: "userTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_labelItems_LabelId",
                schema: "tasks",
                table: "labelItems",
                column: "LabelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachments",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "labelItems",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "notes",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "projects",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "subTasks",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "labels",
                schema: "tasks");

            migrationBuilder.DropTable(
                name: "userTasks",
                schema: "tasks");

            migrationBuilder.DropSequence(
                name: "labelseq",
                schema: "tasks");

            migrationBuilder.DropSequence(
                name: "projectsseq",
                schema: "tasks");

            migrationBuilder.DropSequence(
                name: "taskseq",
                schema: "tasks");
        }
    }
}
