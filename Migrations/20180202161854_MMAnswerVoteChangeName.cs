using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dojoQA.Migrations
{
    public partial class MMAnswerVoteChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVote_Answers_AnswerId",
                table: "AnswerVote");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVote_AspNetUsers_UserId",
                table: "AnswerVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerVote",
                table: "AnswerVote");

            migrationBuilder.RenameTable(
                name: "AnswerVote",
                newName: "AnswerVotes");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVote_UserId",
                table: "AnswerVotes",
                newName: "IX_AnswerVotes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVote_AnswerId",
                table: "AnswerVotes",
                newName: "IX_AnswerVotes_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerVotes",
                table: "AnswerVotes",
                column: "AnswerVoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVotes_Answers_AnswerId",
                table: "AnswerVotes",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVotes_AspNetUsers_UserId",
                table: "AnswerVotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVotes_Answers_AnswerId",
                table: "AnswerVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVotes_AspNetUsers_UserId",
                table: "AnswerVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerVotes",
                table: "AnswerVotes");

            migrationBuilder.RenameTable(
                name: "AnswerVotes",
                newName: "AnswerVote");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVotes_UserId",
                table: "AnswerVote",
                newName: "IX_AnswerVote_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVotes_AnswerId",
                table: "AnswerVote",
                newName: "IX_AnswerVote_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerVote",
                table: "AnswerVote",
                column: "AnswerVoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVote_Answers_AnswerId",
                table: "AnswerVote",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVote_AspNetUsers_UserId",
                table: "AnswerVote",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
