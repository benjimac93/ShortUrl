﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Tedd.ShortUrl.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortUrl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    CreatorAccessToken = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ExpiresUtc = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MetaData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortUrlVisitLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessTimeUtc = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    ClientIp = table.Column<byte[]>(type: "varbinary(16)", maxLength: 16, nullable: true),
                    ShortUrlId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrlVisitLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortUrlVisitLog_ShortUrl_ShortUrlId",
                        column: x => x.ShortUrlId,
                        principalTable: "ShortUrl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrl_Key",
                table: "ShortUrl",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrlVisitLog_ShortUrlId",
                table: "ShortUrlVisitLog",
                column: "ShortUrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortUrlVisitLog");

            migrationBuilder.DropTable(
                name: "ShortUrl");
        }
    }
}
