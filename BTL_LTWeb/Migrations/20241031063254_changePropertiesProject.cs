using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL_LTWeb.Migrations
{
    public partial class changePropertiesProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b18fb4d4-9541-4096-a728-496f769ec947"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("473c2a9e-320e-4194-8672-d9929f90588a"), new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("473c2a9e-320e-4194-8672-d9929f90588a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "Priority" },
                values: new object[] { new Guid("66f4839b-c138-411f-98b0-626b15f1494c"), "user", "USER", 0 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "Priority" },
                values: new object[] { new Guid("eb11cae3-f901-405d-a9bd-6536babf9a30"), "admin", "ADMIN", 9999 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Birthday", "CreatedAt", "Email", "FullName", "PasswordHash", "PhoneNumber" },
                values: new object[] { new Guid("b7453166-24f7-4ca9-b20c-789c32ff323c"), "Khoa CNTT", null, new DateTime(2024, 10, 31, 6, 32, 54, 318, DateTimeKind.Utc).AddTicks(3287), "admin@fithou.com", "Administrator", "AQAAAAEAACcQAAAAECy4aFBAe4MBiTmFAEbOMwfcywFksRi0Xu6OOP4O5XX2QqJLOejWH9i/DeZkxZCS6Q==", "0123456789" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("eb11cae3-f901-405d-a9bd-6536babf9a30"), new Guid("b7453166-24f7-4ca9-b20c-789c32ff323c") });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("66f4839b-c138-411f-98b0-626b15f1494c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("eb11cae3-f901-405d-a9bd-6536babf9a30"), new Guid("b7453166-24f7-4ca9-b20c-789c32ff323c") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eb11cae3-f901-405d-a9bd-6536babf9a30"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7453166-24f7-4ca9-b20c-789c32ff323c"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "Priority" },
                values: new object[] { new Guid("473c2a9e-320e-4194-8672-d9929f90588a"), "admin", "ADMIN", 9999 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "Priority" },
                values: new object[] { new Guid("b18fb4d4-9541-4096-a728-496f769ec947"), "user", "USER", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Birthday", "CreatedAt", "Email", "FullName", "PasswordHash", "PhoneNumber" },
                values: new object[] { new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec"), "Khoa CNTT", null, new DateTime(2024, 10, 30, 3, 48, 25, 924, DateTimeKind.Utc).AddTicks(2897), "admin@fithou.com", "Administrator", "AQAAAAEAACcQAAAAEAJFLXXlb97B1RA1Uj49grORsHKEBplbo5Bb/yw04vdkq/uGO9CoMjNodQQMygrjwg==", "0123456789" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("473c2a9e-320e-4194-8672-d9929f90588a"), new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec") });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
