using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FlexibleCreationOfProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAssignments_Teams_TeamId",
                table: "ProjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Teams_LeadTeamId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUpdates_TeamMembers_CreatedByUserId_CreatedByTeamId",
                table: "ProjectUpdates");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_TeamMembers_AuthorUserId_AuthorTeamId",
                table: "TaskComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Projects_ProjectId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_ProjectId",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TaskComments_AuthorUserId_AuthorTeamId",
                table: "TaskComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectUpdates_CreatedByUserId_CreatedByTeamId",
                table: "ProjectUpdates");

            migrationBuilder.DropIndex(
                name: "IX_Projects_LeadTeamId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectAssignments",
                table: "ProjectAssignments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "AuthorTeamId",
                table: "TaskComments");

            migrationBuilder.DropColumn(
                name: "AuthorUserId",
                table: "TaskComments");

            migrationBuilder.DropColumn(
                name: "CreatedByTeamId",
                table: "ProjectUpdates");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProjectUpdates");

            migrationBuilder.DropColumn(
                name: "LeadTeamId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "ProjectAssignments");

            migrationBuilder.DropColumn(
                name: "AllocationPercentage",
                table: "ProjectAssignments");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "TeamMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "TeamMembers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeveloperId",
                table: "ProjectAssignments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "AllocatedHoursPerWeek",
                table: "ProjectAssignments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TeamMembers_UserId",
                table: "TeamMembers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectAssignments",
                table: "ProjectAssignments",
                columns: new[] { "DeveloperId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_AppUserId1",
                table: "TeamMembers",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_AuthorId",
                table: "TaskComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUpdates_CreatedById",
                table: "ProjectUpdates",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAssignments_TeamMembers_DeveloperId",
                table: "ProjectAssignments",
                column: "DeveloperId",
                principalTable: "TeamMembers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUpdates_TeamMembers_CreatedById",
                table: "ProjectUpdates",
                column: "CreatedById",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_TeamMembers_AuthorId",
                table: "TaskComments",
                column: "AuthorId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Users_AppUserId1",
                table: "TeamMembers",
                column: "AppUserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAssignments_TeamMembers_DeveloperId",
                table: "ProjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUpdates_TeamMembers_CreatedById",
                table: "ProjectUpdates");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_TeamMembers_AuthorId",
                table: "TaskComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Users_AppUserId1",
                table: "TeamMembers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TeamMembers_UserId",
                table: "TeamMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_AppUserId1",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TaskComments_AuthorId",
                table: "TaskComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectUpdates_CreatedById",
                table: "ProjectUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectAssignments",
                table: "ProjectAssignments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "ProjectAssignments");

            migrationBuilder.DropColumn(
                name: "AllocatedHoursPerWeek",
                table: "ProjectAssignments");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "TeamMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "TeamMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorTeamId",
                table: "TaskComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserId",
                table: "TaskComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByTeamId",
                table: "ProjectUpdates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ProjectUpdates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "LeadTeamId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "ProjectAssignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "AllocationPercentage",
                table: "ProjectAssignments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectAssignments",
                table: "ProjectAssignments",
                columns: new[] { "TeamId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_ProjectId",
                table: "TeamMembers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_AuthorUserId_AuthorTeamId",
                table: "TaskComments",
                columns: new[] { "AuthorUserId", "AuthorTeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUpdates_CreatedByUserId_CreatedByTeamId",
                table: "ProjectUpdates",
                columns: new[] { "CreatedByUserId", "CreatedByTeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LeadTeamId",
                table: "Projects",
                column: "LeadTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAssignments_Teams_TeamId",
                table: "ProjectAssignments",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Teams_LeadTeamId",
                table: "Projects",
                column: "LeadTeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUpdates_TeamMembers_CreatedByUserId_CreatedByTeamId",
                table: "ProjectUpdates",
                columns: new[] { "CreatedByUserId", "CreatedByTeamId" },
                principalTable: "TeamMembers",
                principalColumns: new[] { "UserId", "TeamId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_TeamMembers_AuthorUserId_AuthorTeamId",
                table: "TaskComments",
                columns: new[] { "AuthorUserId", "AuthorTeamId" },
                principalTable: "TeamMembers",
                principalColumns: new[] { "UserId", "TeamId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Projects_ProjectId",
                table: "TeamMembers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
