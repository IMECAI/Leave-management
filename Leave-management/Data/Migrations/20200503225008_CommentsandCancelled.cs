﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Leave_management.Data.Migrations
{
    public partial class CommentsandCancelled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateActioned",
                table: "LeaveRequests",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "LeaveRequests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LeaveRequestViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestingEmployeeId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    DateActioned = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: true),
                    ApprovedById = table.Column<string>(nullable: true),
                    Cancelled = table.Column<bool>(nullable: false),
                    RequestComments = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequestViewModel_EmployeeViewModel_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "EmployeeViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequestViewModel_LeaveTypeViewModel_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypeViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequestViewModel_EmployeeViewModel_RequestingEmployeeId",
                        column: x => x.RequestingEmployeeId,
                        principalTable: "EmployeeViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestViewModel_ApprovedById",
                table: "LeaveRequestViewModel",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestViewModel_LeaveTypeId",
                table: "LeaveRequestViewModel",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestViewModel_RequestingEmployeeId",
                table: "LeaveRequestViewModel",
                column: "RequestingEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequestViewModel");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "RequestComments",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateActioned",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
