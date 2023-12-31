﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPRN.Common.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWhatHaveYouDonefieldtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DoneWaste",
                table: "WasteJourney",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DoneWaste",
                table: "WasteJourney",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
