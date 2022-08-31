using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp.DataCenter.Migrations
{
    public partial class addName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfigName",
                table: "ExcelUploadConfigMaster",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigName",
                table: "ExcelUploadConfigMaster");
        }
    }
}
