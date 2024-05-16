using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class in1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e5c30779-587a-46a2-921c-53c4daaa6f6b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f1edd2a6-6edd-464b-b752-d60873d13dd5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fffa10d2-899d-48e6-9e64-29e13f76a951"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6f72194c-3806-4398-bbd3-8c5e73a15d21"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("052c78e9-1e99-4bff-8d57-5f152a79fa84"), null, "Employee", "EMPLOYEE" },
                    { new Guid("95238d51-f271-40b9-b147-c0c7d00e14ed"), null, "Admin", "ADMIN" },
                    { new Guid("c84a6f84-852a-467c-92d5-1ef666205b3f"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("3aa9eab5-bfdb-453a-94d7-7d19e2cd5956"), 0, "e273ce96-dd4a-431f-9c46-ed750f3e4b3c", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEHhc2tk8/2lw/REiRawv99zHD+MolS1A2NnWm16AKFICcmrZJ5+zSGvXusfpC9EJCg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "371582d6-5a9b-45ff-a5f9-1c24d08a5a57", false, "admin@gmail.com", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("052c78e9-1e99-4bff-8d57-5f152a79fa84"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("95238d51-f271-40b9-b147-c0c7d00e14ed"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c84a6f84-852a-467c-92d5-1ef666205b3f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3aa9eab5-bfdb-453a-94d7-7d19e2cd5956"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("e5c30779-587a-46a2-921c-53c4daaa6f6b"), null, "Admin", "ADMIN" },
                    { new Guid("f1edd2a6-6edd-464b-b752-d60873d13dd5"), null, "Employee", "EMPLOYEE" },
                    { new Guid("fffa10d2-899d-48e6-9e64-29e13f76a951"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("6f72194c-3806-4398-bbd3-8c5e73a15d21"), 0, "50990762-a2d7-428c-be15-df8a0370595d", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEMG+S7KrAvShSQxsyAWT/sNrJLdrAvmG8BH8rHvUlZ1dyrrbj87iuoN+5CCrXwIoDQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin@gmail.com", null });
        }
    }
}
