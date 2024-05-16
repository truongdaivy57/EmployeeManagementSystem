using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17bfefc6-5547-45f9-b216-4973d8bdfb5d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c7d1e22c-7365-4faf-a553-3af7311c0732"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb3a448e-b257-4d96-861e-23b72507c946"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("daee436e-5719-4c13-9234-3b506f4d598f"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2613ccd3-1e63-4271-8f40-59ddb760ab3c"), null, "Employee", "EMPLOYEE" },
                    { new Guid("8c22763f-012a-45a0-8d61-d3ff11aa11ce"), null, "Manager", "MANAGER" },
                    { new Guid("a3d77ee3-d303-49a4-acb3-89065d9f1542"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("6c8be045-525d-445f-b933-3575b06f5bbd"), 0, "fb8e1fe3-e3ff-46d1-91ab-87e87d2e65cf", null, "admin@gmail.com", true, "admin", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", null, null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2613ccd3-1e63-4271-8f40-59ddb760ab3c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8c22763f-012a-45a0-8d61-d3ff11aa11ce"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a3d77ee3-d303-49a4-acb3-89065d9f1542"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6c8be045-525d-445f-b933-3575b06f5bbd"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("17bfefc6-5547-45f9-b216-4973d8bdfb5d"), null, "Employee", "EMPLOYEE" },
                    { new Guid("c7d1e22c-7365-4faf-a553-3af7311c0732"), null, "Manager", "MANAGER" },
                    { new Guid("fb3a448e-b257-4d96-861e-23b72507c946"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("daee436e-5719-4c13-9234-3b506f4d598f"), 0, "800c2b29-4058-4a82-bde6-8507d04a1959", null, "admin@gmail.com", false, "admin", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", null, null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }
    }
}
