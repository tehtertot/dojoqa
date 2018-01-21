using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dojoQA.Migrations
{
    public partial class AddedDojoLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DojoId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dojo",
                columns: table => new
                {
                    DojoId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dojo", x => x.DojoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DojoId",
                table: "AspNetUsers",
                column: "DojoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Dojo_DojoId",
                table: "AspNetUsers",
                column: "DojoId",
                principalTable: "Dojo",
                principalColumn: "DojoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Dojo_DojoId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Dojo");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DojoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DojoId",
                table: "AspNetUsers");
        }
    }
}
