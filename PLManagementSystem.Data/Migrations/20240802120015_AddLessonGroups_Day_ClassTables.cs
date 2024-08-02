using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PLManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonGroups_Day_ClassTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dayes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dayes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonGroups_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonGroups_Dayes_DayId",
                        column: x => x.DayId,
                        principalTable: "Dayes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "IsDeleted", "Name", "OrderNo", "ShortName" },
                values: new object[,]
                {
                    { 1, false, "Third Grade Primary", 1, "Third Primary" },
                    { 2, false, "Fourth Grade Primary", 2, "Fourth Primary" },
                    { 3, false, "Fifth Grade Primary", 3, "Fifth Primary" },
                    { 4, false, "Sixth Grade Primary", 4, "Sixth Primary" },
                    { 5, false, "First Grade Preparatory", 5, "First Prep" },
                    { 6, false, "Second Grade Preparatory", 6, "Second Prep" },
                    { 7, false, "Third Grade Preparatory", 7, "Third Prep" },
                    { 8, false, "First Grade Secondary", 8, "First Secondary" },
                    { 9, false, "Second Grade Secondary", 9, "Second Secondary" },
                    { 10, false, "Third Grade Secondary", 10, "Third Secondary" },
                    { 11, false, "First Grade Primary", 11, "First Primary" },
                    { 12, false, "Second Grade Primary", 12, "Second Primary" }
                });

            migrationBuilder.InsertData(
                table: "Dayes",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Saturday" },
                    { 2, false, "Sunday" },
                    { 3, false, "Monday" },
                    { 4, false, "Tuesday" },
                    { 5, false, "Wednesday" },
                    { 6, false, "Thursday" },
                    { 7, false, "Friday" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroups_ClassId",
                table: "LessonGroups",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroups_DayId",
                table: "LessonGroups",
                column: "DayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonGroups");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Dayes");
        }
    }
}
