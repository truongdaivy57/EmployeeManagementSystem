﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("55a1f933-d4ce-4a40-aa8c-68915d4e6877"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("78f981fa-1bb1-4fa4-a844-cc2702d3fc93"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("92426fd8-dfbf-4626-ade6-a8a6a05c5f19"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5ad16b65-7953-4005-bbf2-fb6c75a28f27"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7736eb93-da2c-415c-84da-268f768951bb"), null, "Manager", "MANAGER" },
                    { new Guid("c2568ef1-0303-4abd-a12b-da535a14930d"), null, "Employee", "EMPLOYEE" },
                    { new Guid("ec7bf05a-86cb-4d35-bc0a-ea7ca4a860db"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("f4698dae-6762-4cb8-90cf-3753b7776adc"), 0, "ccef46f1-15e3-4c85-9994-973389391d0d", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIlc4n0IBJ3hPycasgygmpgvzh4d+OGXhKjl0dL9tT7e6MxmbG66HvBqw7eFcUCjkg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin@gmail.com", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7736eb93-da2c-415c-84da-268f768951bb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2568ef1-0303-4abd-a12b-da535a14930d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec7bf05a-86cb-4d35-bc0a-ea7ca4a860db"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f4698dae-6762-4cb8-90cf-3753b7776adc"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("55a1f933-d4ce-4a40-aa8c-68915d4e6877"), null, "Admin", "ADMIN" },
                    { new Guid("78f981fa-1bb1-4fa4-a844-cc2702d3fc93"), null, "Employee", "EMPLOYEE" },
                    { new Guid("92426fd8-dfbf-4626-ade6-a8a6a05c5f19"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("5ad16b65-7953-4005-bbf2-fb6c75a28f27"), 0, "01a91f12-cea1-4d0c-b9a3-087d2936ec08", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIpAIztSrBCAj58zv2vrdWwavfGpITCgfdnmZdpOYUQeNlTauHuEiUJsA31sl7Kl9Q==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }
    }
}
