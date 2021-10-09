using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bcvp.Blog.Core.Migrations
{
    public partial class AddBlogUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPosts_AppBlogs_BlogId",
                table: "AppPosts");

            migrationBuilder.DropIndex(
                name: "IX_AppPosts_BlogId",
                table: "AppPosts");

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AppPosts_BlogId",
                table: "AppPosts",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPosts_AppBlogs_BlogId",
                table: "AppPosts",
                column: "BlogId",
                principalTable: "AppBlogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
