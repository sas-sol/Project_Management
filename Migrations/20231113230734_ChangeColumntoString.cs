using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Management.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumntoString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Project_Id_MIS",
                table: "ProjectApiUrl",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Project_Id_MIS",
                table: "ProjectApiUrl",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
