using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("36eabb0b-4b2a-4519-8978-010bedc41922"), null, "Employee", "EMPLOYEE" },
                    { new Guid("a097fd8e-ab0e-4a18-92d2-22cf352ce6c1"), null, "Admin", "ADMIN" },
                    { new Guid("a57a1cc2-1949-403d-8dc2-2d474721f6d6"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("a8332bd1-17d9-4277-b996-0030abb7dedc"), 0, "4dc37826-f7cb-40ce-a234-6c9e9ca34a01", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", null, null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("36eabb0b-4b2a-4519-8978-010bedc41922"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a097fd8e-ab0e-4a18-92d2-22cf352ce6c1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a57a1cc2-1949-403d-8dc2-2d474721f6d6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a8332bd1-17d9-4277-b996-0030abb7dedc"));

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
    }
}
