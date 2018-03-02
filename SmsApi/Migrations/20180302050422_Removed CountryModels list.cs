using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmsApi.Migrations
{
    public partial class RemovedCountryModelslist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sms_Country_id",
                table: "Sms");

            migrationBuilder.DropIndex(
                name: "IX_Sms_id",
                table: "Sms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sms_id",
                table: "Sms",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sms_Country_id",
                table: "Sms",
                column: "id",
                principalTable: "Country",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
