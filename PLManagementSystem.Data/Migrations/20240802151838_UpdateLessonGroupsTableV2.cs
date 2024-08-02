using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLessonGroupsTableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                table: "LessonGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "LessonGroups");
        }
    }
}
