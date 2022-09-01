using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp.DataCenter.Migrations
{
    public partial class addExportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SheelName",
                table: "ExcelUploadConfigMaster",
                newName: "SheetName");

            migrationBuilder.CreateTable(
                name: "ExcelExportConfigMaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SheetName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ShowRowNo = table.Column<bool>(type: "bit", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelExportConfigMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcelExportConfigItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelExportConfigItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelExportConfigItem_ExcelExportConfigMaster_ConfigId",
                        column: x => x.ConfigId,
                        principalTable: "ExcelExportConfigMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcelExportConfigItem_ConfigId",
                table: "ExcelExportConfigItem",
                column: "ConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelExportConfigItem");

            migrationBuilder.DropTable(
                name: "ExcelExportConfigMaster");

            migrationBuilder.RenameColumn(
                name: "SheetName",
                table: "ExcelUploadConfigMaster",
                newName: "SheelName");
        }
    }
}
