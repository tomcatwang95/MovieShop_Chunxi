﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CreatePurchaseMovieRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Purchase_MovieId",
                table: "Purchase",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Movie_MovieId",
                table: "Purchase",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Movie_MovieId",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_MovieId",
                table: "Purchase");
        }
    }
}
