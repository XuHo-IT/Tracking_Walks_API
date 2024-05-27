using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalk.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Diffilculties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e5602235-bb76-411e-a405-db796c5e875f"), "Medium" },
                    { new Guid("e58c00c3-ddd0-4353-91c8-9a13a834c586"), "Easy" },
                    { new Guid("ee8072e0-0d0f-4db5-bc29-8bac82fede45"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("12b1e65a-36e2-47d3-9392-5e2b26b91acb"), "700000", "Sai Gon", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcStgxAi3_mA2naEA1Q7zWTSxB26xj6xh3g3piRRG_dvEQ&s" },
                    { new Guid("98fdcbf9-50d6-4a45-97d6-9244fd266647"), "550000", "Da Nang", "https://tourism.danang.vn/wp-content/uploads/2023/02/tour-du-lich-da-nang-1.jpg" },
                    { new Guid("9cdc6148-b11c-4b00-ba0e-84a87287e5f7"), "100000", "Ha Noi", "https://photo-cms-baophapluat.epicdn.me/w840/Uploaded/2024/athlraqhpghat/2023_06_25/ho-hoan-kiem-7185.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Diffilculties",
                keyColumn: "Id",
                keyValue: new Guid("e5602235-bb76-411e-a405-db796c5e875f"));

            migrationBuilder.DeleteData(
                table: "Diffilculties",
                keyColumn: "Id",
                keyValue: new Guid("e58c00c3-ddd0-4353-91c8-9a13a834c586"));

            migrationBuilder.DeleteData(
                table: "Diffilculties",
                keyColumn: "Id",
                keyValue: new Guid("ee8072e0-0d0f-4db5-bc29-8bac82fede45"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("12b1e65a-36e2-47d3-9392-5e2b26b91acb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("98fdcbf9-50d6-4a45-97d6-9244fd266647"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9cdc6148-b11c-4b00-ba0e-84a87287e5f7"));
        }
    }
}
