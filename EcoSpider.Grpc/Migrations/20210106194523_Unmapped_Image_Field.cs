using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoSpider.Grpc.Migrations
{
    public partial class Unmapped_Image_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Products",
                type: "bytea",
                nullable: true);
        }
    }
}
