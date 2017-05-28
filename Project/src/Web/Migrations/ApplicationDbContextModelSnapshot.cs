﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Wjw1.Infrastructure;

namespace Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysAction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<bool>("Enable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<bool>("System");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SysActions");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysArea", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<bool>("Enable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("SysAreas");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysBroadcast", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<DateTime?>("AbsoluteExpirationUtcDateTime");

                    b.Property<string>("AddresseeId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Picture")
                        .HasMaxLength(200);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysBroadcasts");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysBroadcastReceived", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("SysBroadcastId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("SysBroadcastId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysBroadcastReceiveds");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysCache", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(449);

                    b.Property<DateTimeOffset?>("AbsoluteExpiration");

                    b.Property<DateTimeOffset>("ExpiresAtTime");

                    b.Property<long?>("SlidingExpirationInSeconds");

                    b.Property<byte[]>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SysCaches");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysController", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("ActionName")
                        .HasMaxLength(50);

                    b.Property<string>("ControllerName")
                        .HasMaxLength(50);

                    b.Property<bool>("Display");

                    b.Property<bool>("Enable");

                    b.Property<string>("Ico");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Parameter")
                        .HasMaxLength(50);

                    b.Property<string>("SysAreaId")
                        .IsRequired();

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("TargetBlank");

                    b.HasKey("Id");

                    b.HasIndex("SysAreaId");

                    b.ToTable("SysControllers");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysControllerSysAction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("SysActionId")
                        .IsRequired();

                    b.Property<string>("SysControllerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SysActionId");

                    b.HasIndex("SysControllerId");

                    b.ToTable("SysControllerSysActions");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysDepartment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enable");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysDepartments");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysDepartmentSysUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("SysDepartmentId")
                        .IsRequired();

                    b.Property<string>("SysUserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SysDepartmentId");

                    b.HasIndex("SysUserId");

                    b.ToTable("SysDepartmentSysUser");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysEnterprise", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("EnterpriseName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("SysEnterprises");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysEnterpriseSysUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("SysEnterpriseId")
                        .IsRequired();

                    b.Property<string>("SysUserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SysEnterpriseId");

                    b.HasIndex("SysUserId");

                    b.ToTable("SysEnterpriseSysUsers");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysHelp", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<int>("Sort");

                    b.Property<string>("SysHelpClassId")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("SysHelpClassId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysHelps");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysHelpClass", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enable");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysHelpClasses");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysKeyword", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<int>("Count");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enable");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<int>("Type");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysKeywords");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysRoleSysControllerSysAction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.Property<string>("SysControllerSysActionId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("SysControllerSysActionId");

                    b.ToTable("SysRoleSysControllerSysActions");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysSignalR", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("GroupId")
                        .HasMaxLength(100);

                    b.Property<string>("Message");

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.Property<Guid?>("UserId1");

                    b.Property<string>("UserName")
                        .HasMaxLength(100);

                    b.Property<string>("UserName1")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysSignalRs");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysSignalROnline", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("ConnectionId")
                        .HasMaxLength(100);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("GroupId")
                        .HasMaxLength(100);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysSignalROnlines");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedTime")
                        .HasMaxLength(50);

                    b.Property<string>("CurrentEnterpriseId")
                        .HasMaxLength(450);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName")
                        .HasMaxLength(100);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Picture")
                        .HasMaxLength(300);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysUserLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<double>("ActionDuration");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<double>("Duration");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Ip")
                        .HasMaxLength(100);

                    b.Property<string>("RecordId")
                        .HasMaxLength(450);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("RequestType")
                        .HasMaxLength(64);

                    b.Property<string>("SysAction")
                        .HasMaxLength(40);

                    b.Property<string>("SysArea")
                        .HasMaxLength(40);

                    b.Property<string>("SysController")
                        .HasMaxLength(40);

                    b.Property<string>("SysControllerSysActionId");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .HasMaxLength(1024);

                    b.Property<double>("ViewDuration");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("SysControllerSysActionId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("SysUserLogs");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.VerifyCode", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<DateTime>("AbsoluteExpirationUtcDateTime");

                    b.Property<string>("Code")
                        .HasMaxLength(6);

                    b.Property<DateTime>("CreateUtcDateTime");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Destination")
                        .HasMaxLength(50);

                    b.Property<int>("VerifyCodeUsageType");

                    b.Property<int>("VerifyProvider");

                    b.HasKey("Id");

                    b.ToTable("VerifyCodes");
                });

            modelBuilder.Entity("Wjw1.Module.Localization.Models.Culture", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Localization_Culture");
                });

            modelBuilder.Entity("Wjw1.Module.Localization.Models.Resource", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("CultureId")
                        .IsRequired();

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("Value")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.ToTable("Localization_Resource");
                });

            modelBuilder.Entity("Wjw1.Module.Task.Models.TaskCenter", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(450);

                    b.Property<string>("ActualEndTime")
                        .HasMaxLength(64);

                    b.Property<string>("Content");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("Deleted");

                    b.Property<decimal>("Duration");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(450);

                    b.Property<string>("Files");

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("ScheduleEndTime")
                        .HasMaxLength(64);

                    b.Property<string>("TaskExecutorId")
                        .IsRequired();

                    b.Property<int>("TaskType");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(450);

                    b.Property<string>("UpdatedDate")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("TaskExecutorId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Task_TaskCenter");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole");

                    b.Property<string>("EnterpriseId")
                        .HasMaxLength(256);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("System");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.ToTable("SysRole");

                    b.HasDiscriminator().HasValue("SysRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysBroadcast", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysBroadcastReceived", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysBroadcast", "SysBroadcast")
                        .WithMany("SysBroadcastReceiveds")
                        .HasForeignKey("SysBroadcastId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysController", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysArea", "SysArea")
                        .WithMany("SysControllers")
                        .HasForeignKey("SysAreaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysControllerSysAction", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysAction", "SysAction")
                        .WithMany("SysControllerSysActions")
                        .HasForeignKey("SysActionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysController", "SysController")
                        .WithMany("SysControllerSysActions")
                        .HasForeignKey("SysControllerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysDepartment", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysDepartmentSysUser", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysDepartment", "SysDepartment")
                        .WithMany("SysDepartmentSysUsers")
                        .HasForeignKey("SysDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "SysUser")
                        .WithMany("SysDepartmentSysUsers")
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysEnterpriseSysUser", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysEnterprise", "SysEnterprise")
                        .WithMany("SysEnterpriseSysUsers")
                        .HasForeignKey("SysEnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "SysUser")
                        .WithMany("SysEnterpriseSysUsers")
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysHelp", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysHelpClass", "SysHelpClass")
                        .WithMany("SysHelps")
                        .HasForeignKey("SysHelpClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysHelpClass", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysKeyword", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysRoleSysControllerSysAction", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysRole", "SysRole")
                        .WithMany("SysRoleSysControllerSysActions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysControllerSysAction", "SysControllerSysAction")
                        .WithMany("SysRoleSysControllerSysActions")
                        .HasForeignKey("SysControllerSysActionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysSignalR", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysSignalROnline", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Infrastructure.Models.SysUserLog", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysControllerSysAction", "SysControllerSysAction")
                        .WithMany()
                        .HasForeignKey("SysControllerSysActionId");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Wjw1.Module.Localization.Models.Resource", b =>
                {
                    b.HasOne("Wjw1.Module.Localization.Models.Culture", "Culture")
                        .WithMany("Resources")
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wjw1.Module.Task.Models.TaskCenter", b =>
                {
                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "TaskExecutor")
                        .WithMany()
                        .HasForeignKey("TaskExecutorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wjw1.Infrastructure.Models.SysUser", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });
        }
    }
}
