using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHistoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "History",
                newName: "TotalAttempts");

            migrationBuilder.RenameColumn(
                name: "LastInteractionTime",
                table: "History",
                newName: "LastAttemptAt");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "History",
                newName: "IsCorrect");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "History",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "AttemptsToSolve",
                table: "History",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstAttemptAt",
                table: "History",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstSolvedAt",
                table: "History",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                columns: new[] { "UserId", "ProblemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.DropColumn(
                name: "AttemptsToSolve",
                table: "History");

            migrationBuilder.DropColumn(
                name: "FirstAttemptAt",
                table: "History");

            migrationBuilder.DropColumn(
                name: "FirstSolvedAt",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "TotalAttempts",
                table: "History",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "LastAttemptAt",
                table: "History",
                newName: "LastInteractionTime");

            migrationBuilder.RenameColumn(
                name: "IsCorrect",
                table: "History",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "History",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                column: "Id");
        }
    }
}
