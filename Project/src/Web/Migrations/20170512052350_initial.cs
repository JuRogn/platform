using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysActions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    ActionName = table.Column<string>(maxLength: 40, nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    System = table.Column<bool>(nullable: false),
                    SystemId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysAreas",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    AreaName = table.Column<string>(maxLength: 40, nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    SystemId = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysCaches",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 449, nullable: false),
                    AbsoluteExpiration = table.Column<DateTimeOffset>(nullable: true),
                    ExpiresAtTime = table.Column<DateTimeOffset>(nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(nullable: true),
                    Value = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysCaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysEnterprises",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    EnterpriseName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysEnterprises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysLanguagePacks",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Language = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Value = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLanguagePacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedTime = table.Column<string>(maxLength: 50, nullable: true),
                    CurrentEnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    Picture = table.Column<string>(maxLength: 300, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerifyCodes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    AbsoluteExpirationUtcDateTime = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(maxLength: 6, nullable: true),
                    CreateUtcDateTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Destination = table.Column<string>(maxLength: 50, nullable: true),
                    VerifyCodeUsageType = table.Column<int>(nullable: false),
                    VerifyProvider = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    RoleName = table.Column<string>(maxLength: 100, nullable: true),
                    SysDefault = table.Column<bool>(nullable: true),
                    SystemId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "SysControllers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    ActionName = table.Column<string>(maxLength: 50, nullable: true),
                    ControllerName = table.Column<string>(maxLength: 50, nullable: true),
                    Display = table.Column<bool>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    Ico = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Parameter = table.Column<string>(maxLength: 50, nullable: true),
                    SysAreaId = table.Column<string>(nullable: false),
                    SystemId = table.Column<string>(maxLength: 50, nullable: false),
                    TargetBlank = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysControllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysControllers_SysAreas_SysAreaId",
                        column: x => x.SysAreaId,
                        principalTable: "SysAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysBroadcasts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    AbsoluteExpirationUtcDateTime = table.Column<DateTime>(nullable: true),
                    AddresseeId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Picture = table.Column<string>(maxLength: 200, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysBroadcasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysBroadcasts_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysBroadcasts_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysDepartments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    SystemId = table.Column<string>(maxLength: 30, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysDepartments_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysDepartments_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysEnterpriseSysUsers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    SysEnterpriseId = table.Column<string>(nullable: false),
                    SysUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysEnterpriseSysUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysEnterpriseSysUsers_SysEnterprises_SysEnterpriseId",
                        column: x => x.SysEnterpriseId,
                        principalTable: "SysEnterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysEnterpriseSysUsers_AspNetUsers_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysHelpClasses",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    SystemId = table.Column<string>(maxLength: 30, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysHelpClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysHelpClasses_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysHelpClasses_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysKeywords",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Count = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Keyword = table.Column<string>(maxLength: 40, nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysKeywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysKeywords_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysKeywords_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysSignalRs",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    GroupId = table.Column<string>(maxLength: 100, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true),
                    UserId1 = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    UserName1 = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysSignalRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysSignalRs_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysSignalRs_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysSignalROnlines",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    ConnectionId = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    GroupId = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysSignalROnlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysSignalROnlines_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysSignalROnlines_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysControllerSysActions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    SysActionId = table.Column<string>(nullable: false),
                    SysControllerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysControllerSysActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysControllerSysActions_SysActions_SysActionId",
                        column: x => x.SysActionId,
                        principalTable: "SysActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysControllerSysActions_SysControllers_SysControllerId",
                        column: x => x.SysControllerId,
                        principalTable: "SysControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysBroadcastReceiveds",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    SysBroadcastId = table.Column<string>(maxLength: 450, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysBroadcastReceiveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysBroadcastReceiveds_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysBroadcastReceiveds_SysBroadcasts_SysBroadcastId",
                        column: x => x.SysBroadcastId,
                        principalTable: "SysBroadcasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysBroadcastReceiveds_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysDepartmentSysUser",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    SysDepartmentId = table.Column<string>(nullable: false),
                    SysUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDepartmentSysUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysDepartmentSysUser_SysDepartments_SysDepartmentId",
                        column: x => x.SysDepartmentId,
                        principalTable: "SysDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysDepartmentSysUser_AspNetUsers_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysHelps",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    SysHelpClassId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysHelps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysHelps_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysHelps_SysHelpClasses_SysHelpClassId",
                        column: x => x.SysHelpClassId,
                        principalTable: "SysHelpClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysHelps_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleSysControllerSysActions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    SysControllerSysActionId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleSysControllerSysActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRoleSysControllerSysActions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysRoleSysControllerSysActions_SysControllerSysActions_SysControllerSysActionId",
                        column: x => x.SysControllerSysActionId,
                        principalTable: "SysControllerSysActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysUserLogs",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    ActionDuration = table.Column<double>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Ip = table.Column<string>(maxLength: 100, nullable: true),
                    RecordId = table.Column<string>(maxLength: 450, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    RequestType = table.Column<string>(maxLength: 64, nullable: true),
                    SysAction = table.Column<string>(maxLength: 40, nullable: true),
                    SysArea = table.Column<string>(maxLength: 40, nullable: true),
                    SysController = table.Column<string>(maxLength: 40, nullable: true),
                    SysControllerSysActionId = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(maxLength: 1024, nullable: true),
                    ViewDuration = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysUserLogs_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysUserLogs_SysControllerSysActions_SysControllerSysActionId",
                        column: x => x.SysControllerSysActionId,
                        principalTable: "SysControllerSysActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SysUserLogs_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysBroadcasts_CreatedBy",
                table: "SysBroadcasts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysBroadcasts_UpdatedBy",
                table: "SysBroadcasts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysBroadcastReceiveds_CreatedBy",
                table: "SysBroadcastReceiveds",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysBroadcastReceiveds_SysBroadcastId",
                table: "SysBroadcastReceiveds",
                column: "SysBroadcastId");

            migrationBuilder.CreateIndex(
                name: "IX_SysBroadcastReceiveds_UpdatedBy",
                table: "SysBroadcastReceiveds",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllers_SysAreaId",
                table: "SysControllers",
                column: "SysAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllerSysActions_SysActionId",
                table: "SysControllerSysActions",
                column: "SysActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SysControllerSysActions_SysControllerId",
                table: "SysControllerSysActions",
                column: "SysControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartments_CreatedBy",
                table: "SysDepartments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartments_UpdatedBy",
                table: "SysDepartments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartmentSysUser_SysDepartmentId",
                table: "SysDepartmentSysUser",
                column: "SysDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SysDepartmentSysUser_SysUserId",
                table: "SysDepartmentSysUser",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysEnterpriseSysUsers_SysEnterpriseId",
                table: "SysEnterpriseSysUsers",
                column: "SysEnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_SysEnterpriseSysUsers_SysUserId",
                table: "SysEnterpriseSysUsers",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_CreatedBy",
                table: "SysHelps",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_SysHelpClassId",
                table: "SysHelps",
                column: "SysHelpClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelps_UpdatedBy",
                table: "SysHelps",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelpClasses_CreatedBy",
                table: "SysHelpClasses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysHelpClasses_UpdatedBy",
                table: "SysHelpClasses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysKeywords_CreatedBy",
                table: "SysKeywords",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysKeywords_UpdatedBy",
                table: "SysKeywords",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleSysControllerSysActions_RoleId",
                table: "SysRoleSysControllerSysActions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleSysControllerSysActions_SysControllerSysActionId",
                table: "SysRoleSysControllerSysActions",
                column: "SysControllerSysActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SysSignalRs_CreatedBy",
                table: "SysSignalRs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysSignalRs_UpdatedBy",
                table: "SysSignalRs",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysSignalROnlines_CreatedBy",
                table: "SysSignalROnlines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysSignalROnlines_UpdatedBy",
                table: "SysSignalROnlines",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_CreatedBy",
                table: "SysUserLogs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_SysControllerSysActionId",
                table: "SysUserLogs",
                column: "SysControllerSysActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserLogs_UpdatedBy",
                table: "SysUserLogs",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysBroadcastReceiveds");

            migrationBuilder.DropTable(
                name: "SysCaches");

            migrationBuilder.DropTable(
                name: "SysDepartmentSysUser");

            migrationBuilder.DropTable(
                name: "SysEnterpriseSysUsers");

            migrationBuilder.DropTable(
                name: "SysHelps");

            migrationBuilder.DropTable(
                name: "SysKeywords");

            migrationBuilder.DropTable(
                name: "SysLanguagePacks");

            migrationBuilder.DropTable(
                name: "SysRoleSysControllerSysActions");

            migrationBuilder.DropTable(
                name: "SysSignalRs");

            migrationBuilder.DropTable(
                name: "SysSignalROnlines");

            migrationBuilder.DropTable(
                name: "SysUserLogs");

            migrationBuilder.DropTable(
                name: "VerifyCodes");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "SysBroadcasts");

            migrationBuilder.DropTable(
                name: "SysDepartments");

            migrationBuilder.DropTable(
                name: "SysEnterprises");

            migrationBuilder.DropTable(
                name: "SysHelpClasses");

            migrationBuilder.DropTable(
                name: "SysControllerSysActions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SysActions");

            migrationBuilder.DropTable(
                name: "SysControllers");

            migrationBuilder.DropTable(
                name: "SysAreas");
        }
    }
}
