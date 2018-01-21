using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dojoQA.Migrations
{
    public partial class UpdatedUserOfQandA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_ApplicationUserId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_AuthorId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Questions",
                newName: "AskedById");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_AuthorId",
                table: "Questions",
                newName: "IX_Questions_AskedById");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Answers",
                newName: "AnsweredById");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_ApplicationUserId",
                table: "Answers",
                newName: "IX_Answers_AnsweredById");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_AnsweredById",
                table: "Answers",
                column: "AnsweredById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_AskedById",
                table: "Questions",
                column: "AskedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_AnsweredById",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_AskedById",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "AskedById",
                table: "Questions",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_AskedById",
                table: "Questions",
                newName: "IX_Questions_AuthorId");

            migrationBuilder.RenameColumn(
                name: "AnsweredById",
                table: "Answers",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_AnsweredById",
                table: "Answers",
                newName: "IX_Answers_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_ApplicationUserId",
                table: "Answers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_AuthorId",
                table: "Questions",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
