using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitIt.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_User_CreatedById",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Payment",
                newName: "UserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                newName: "IX_Payment_UserGroupId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Expense",
                newName: "CreatedByUserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_CreatedById",
                table: "Expense",
                newName: "IX_Expense_CreatedByUserGroupId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserGroup",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserGroup",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expense",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExpenseSplit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwedAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseSplit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseSplit_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseSplit_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId_GroupId",
                table: "UserGroup",
                columns: new[] { "UserId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseSplit_ExpenseId_UserGroupId",
                table: "ExpenseSplit",
                columns: new[] { "ExpenseId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseSplit_UserGroupId",
                table: "ExpenseSplit",
                column: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_UserGroup_CreatedByUserGroupId",
                table: "Expense",
                column: "CreatedByUserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserGroup_UserGroupId",
                table: "Payment",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_UserGroup_CreatedByUserGroupId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_UserGroup_UserGroupId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "ExpenseSplit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserGroup_UserId_GroupId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserGroup");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserGroup");

            migrationBuilder.RenameColumn(
                name: "UserGroupId",
                table: "Payment",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_UserGroupId",
                table: "Payment",
                newName: "IX_Payment_UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserGroupId",
                table: "Expense",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_CreatedByUserGroupId",
                table: "Expense",
                newName: "IX_Expense_CreatedById");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expense",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_User_CreatedById",
                table: "Expense",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
