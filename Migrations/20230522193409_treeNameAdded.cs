using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _20aa05db_9a03_490b_94e8_785205bc0dd0.Migrations
{
    /// <inheritdoc />
    public partial class treeNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TreeName",
                table: "Nodes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreeName",
                table: "Nodes");
        }
    }
}
