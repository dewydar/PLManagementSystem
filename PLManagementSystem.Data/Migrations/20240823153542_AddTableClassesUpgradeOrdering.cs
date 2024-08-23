using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PLManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableClassesUpgradeOrdering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassesUpgradeOrdering",
                columns: table => new
                {
                    LowerClassId = table.Column<int>(type: "int", nullable: false),
                    UpperClassId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassesUpgradeOrdering", x => new { x.LowerClassId, x.UpperClassId });
                    table.ForeignKey(
                        name: "FK_ClassesUpgradeOrdering_Classes_LowerClassId",
                        column: x => x.LowerClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassesUpgradeOrdering_Classes_UpperClassId",
                        column: x => x.UpperClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "First Grade Primary", "First Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Second Grade Primary", "Second Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Third Grade Primary", "Third Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Fourth Grade Primary", "Fourth Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Fifth Grade Primary", "Fifth Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Sixth Grade Primary", "Sixth Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "First Grade Preparatory", "First Prep" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Second Grade Preparatory", "Second Prep" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Third Grade Preparatory", "Third Prep" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "First Grade Secondary", "First Secondary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Second Grade Secondary", "Second Secondary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Third Grade Secondary", "Third Secondary" });

            migrationBuilder.InsertData(
                table: "ClassesUpgradeOrdering",
                columns: new[] { "LowerClassId", "UpperClassId", "IsDeleted" },
                values: new object[,]
                {
                    { 1, 2, false },
                    { 2, 3, false },
                    { 3, 4, false },
                    { 4, 5, false },
                    { 5, 6, false },
                    { 6, 7, false },
                    { 7, 8, false },
                    { 8, 9, false },
                    { 9, 10, false },
                    { 10, 11, false },
                    { 11, 12, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassesUpgradeOrdering_UpperClassId",
                table: "ClassesUpgradeOrdering",
                column: "UpperClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassesUpgradeOrdering");

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Third Grade Primary", "Third Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Fourth Grade Primary", "Fourth Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Fifth Grade Primary", "Fifth Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Sixth Grade Primary", "Sixth Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "First Grade Preparatory", "First Prep" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Second Grade Preparatory", "Second Prep" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Third Grade Preparatory", "Third Prep" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "First Grade Secondary", "First Secondary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Second Grade Secondary", "Second Secondary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Third Grade Secondary", "Third Secondary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "First Grade Primary", "First Primary" });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Second Grade Primary", "Second Primary" });
        }
    }
}
