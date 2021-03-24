using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VisitorsTracker.Db.Migrations
{
    public partial class CorrectionLastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Classes_ClassesId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ClassesId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "Subjects");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectsId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubjectsId",
                table: "Classes",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Subjects_SubjectsId",
                table: "Classes",
                column: "SubjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Subjects_SubjectsId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SubjectsId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SubjectsId",
                table: "Classes");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassesId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassesId",
                table: "Subjects",
                column: "ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Classes_ClassesId",
                table: "Subjects",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
