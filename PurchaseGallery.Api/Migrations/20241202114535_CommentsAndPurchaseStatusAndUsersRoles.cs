using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurchaseGallery.Api.Migrations
{
    /// <inheritdoc />
    public partial class CommentsAndPurchaseStatusAndUsersRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Approvals");

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseRequestId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "PurchaseRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Approvals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_PurchaseRequests_PurchaseRequestId",
                        column: x => x.PurchaseRequestId,
                        principalTable: "PurchaseRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommenterId",
                        column: x => x.CommenterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequestsStauts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestsStauts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PurchaseRequestId",
                table: "Users",
                column: "PurchaseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_StatusId",
                table: "PurchaseRequests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_StatusId",
                table: "Approvals",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommenterId",
                table: "Comments",
                column: "CommenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PurchaseRequestId",
                table: "Comments",
                column: "PurchaseRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_PurchaseRequestsStauts_StatusId",
                table: "Approvals",
                column: "StatusId",
                principalTable: "PurchaseRequestsStauts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestsStauts_StatusId",
                table: "PurchaseRequests",
                column: "StatusId",
                principalTable: "PurchaseRequestsStauts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PurchaseRequests_PurchaseRequestId",
                table: "Users",
                column: "PurchaseRequestId",
                principalTable: "PurchaseRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_PurchaseRequestsStauts_StatusId",
                table: "Approvals");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestsStauts_StatusId",
                table: "PurchaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PurchaseRequests_PurchaseRequestId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PurchaseRequestsStauts");

            migrationBuilder.DropIndex(
                name: "IX_Users_PurchaseRequestId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_StatusId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_Approvals_StatusId",
                table: "Approvals");

            migrationBuilder.DropColumn(
                name: "PurchaseRequestId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Approvals");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PurchaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Approvals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
