using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dojoQA.Migrations
{
    public partial class MMAnswerVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnswerVote",
                columns: table => new
                {
                    AnswerVoteId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVote", x => x.AnswerVoteId);
                    table.ForeignKey(
                        name: "FK_AnswerVote_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerVote_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVote_AnswerId",
                table: "AnswerVote",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVote_UserId",
                table: "AnswerVote",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerVote");

            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Answers");
        }
    }
}
