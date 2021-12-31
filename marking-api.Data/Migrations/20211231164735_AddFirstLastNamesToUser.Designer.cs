﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using marking_api.Data;

#nullable disable

namespace marking_api.Data.Migrations
{
    [DbContext(typeof(MarkingDbContext))]
    [Migration("20211231164735_AddFirstLastNamesToUser")]
    partial class AddFirstLastNamesToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFileDM", b =>
                {
                    b.Property<long>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("FileDescription")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long?>("FolderID")
                        .HasColumnType("bigint");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FileId");

                    b.HasIndex("FolderID");

                    b.ToTable("FSFiles", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFileStateDM", b =>
                {
                    b.Property<long>("FileStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("FileStateDescription")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("FileStateType")
                        .HasColumnType("int");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FileStateId");

                    b.ToTable("FSFileStates", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFileVersionDM", b =>
                {
                    b.Property<long>("FileVersionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("longblob");

                    b.Property<string>("FileExtension")
                        .HasColumnType("longtext");

                    b.Property<long?>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FileStateId")
                        .HasColumnType("bigint");

                    b.Property<string>("FilecontentType")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UploadFileName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FileVersionId");

                    b.HasIndex("FileId");

                    b.HasIndex("FileStateId");

                    b.ToTable("FSFileVersions", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFolderDM", b =>
                {
                    b.Property<long>("FolderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("FolderDescription")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FolderName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long?>("ParentFolderId")
                        .HasColumnType("bigint");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FolderId");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("FSFolders", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFolderFileDM", b =>
                {
                    b.Property<long>("FolderFileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long>("FolderId")
                        .HasColumnType("bigint");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FolderFileId");

                    b.HasIndex("FileId");

                    b.HasIndex("FolderId");

                    b.ToTable("FSFolderFiles", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFolderRoleDM", b =>
                {
                    b.Property<long>("FolderRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("FolderId")
                        .HasColumnType("bigint");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("FolderRoleId");

                    b.HasIndex("FolderId");

                    b.HasIndex("RoleId");

                    b.ToTable("FSFolderRoles", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("RoleId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("RoleDescription")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("IdRoles", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("IdRoleClaims", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.RolePermission", b =>
                {
                    b.Property<long>("RolePermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("PermissionSecurity")
                        .HasColumnType("longtext");

                    b.Property<int>("PermissionType")
                        .HasColumnType("int");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("SiteAreaId")
                        .HasColumnType("bigint");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RolePermissionId");

                    b.HasIndex("RoleId");

                    b.HasIndex("SiteAreaId");

                    b.ToTable("IdRolePermission", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.SiteArea", b =>
                {
                    b.Property<long>("SiteAreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Link")
                        .HasColumnType("longtext");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("SiteAreaId");

                    b.ToTable("IdSiteArea", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("UserId");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("longblob")
                        .HasColumnName("ProfilePicture");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("IdUsers", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IdUserClaims", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("IdUserLogins", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("UserId");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("IdUserRoles", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("UserId");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("IdUserTokens", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.GradeDM", b =>
                {
                    b.Property<long>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<long>("FeedbackId")
                        .HasColumnType("bigint");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("GradeId");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Grades", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.GroupDM", b =>
                {
                    b.Property<long>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("GroupName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.GroupMarkerDM", b =>
                {
                    b.Property<long>("GroupMarkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("GroupMarkerId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMarkers", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.ProjectDM", b =>
                {
                    b.Property<long>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("EthicsAccepted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("EthicsFormId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("longtext");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ProjectId");

                    b.HasIndex("EthicsFormId");

                    b.ToTable("Projects", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.TagDM", b =>
                {
                    b.Property<long>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("TagName")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TagId");

                    b.HasIndex("TagName")
                        .IsUnique();

                    b.ToTable("Tags", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.UserGradeDM", b =>
                {
                    b.Property<long>("UserGradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserGradeId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGrades", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.UserGroupDM", b =>
                {
                    b.Property<long>("UserGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("canDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserGroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups", "dbo");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFileDM", b =>
                {
                    b.HasOne("marking_api.DataModel.FileSystem.FSFolderDM", "Folder")
                        .WithMany()
                        .HasForeignKey("FolderID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFileVersionDM", b =>
                {
                    b.HasOne("marking_api.DataModel.FileSystem.FSFileDM", "File")
                        .WithMany("FileVersions")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("marking_api.DataModel.FileSystem.FSFileStateDM", "FileState")
                        .WithMany("FileVersions")
                        .HasForeignKey("FileStateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("File");

                    b.Navigation("FileState");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFolderDM", b =>
                {
                    b.HasOne("marking_api.DataModel.FileSystem.FSFolderDM", "ParentFolder")
                        .WithMany("ChildFolders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentFolder");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFolderFileDM", b =>
                {
                    b.HasOne("marking_api.DataModel.FileSystem.FSFileDM", "File")
                        .WithMany("FolderFiles")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.FileSystem.FSFolderDM", "Folder")
                        .WithMany("FolderFiles")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFolderRoleDM", b =>
                {
                    b.HasOne("marking_api.DataModel.FileSystem.FSFolderDM", "Folder")
                        .WithMany()
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.Identity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Folder");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.RoleClaim", b =>
                {
                    b.HasOne("marking_api.DataModel.Identity.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.RolePermission", b =>
                {
                    b.HasOne("marking_api.DataModel.Identity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("marking_api.DataModel.Identity.SiteArea", "SiteArea")
                        .WithMany()
                        .HasForeignKey("SiteAreaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("SiteArea");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserClaim", b =>
                {
                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserLogin", b =>
                {
                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserRole", b =>
                {
                    b.HasOne("marking_api.DataModel.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.UserToken", b =>
                {
                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.GradeDM", b =>
                {
                    b.HasOne("marking_api.DataModel.FileSystem.FSFileDM", "Feedback")
                        .WithMany()
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.Project.GroupDM", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Feedback");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.GroupMarkerDM", b =>
                {
                    b.HasOne("marking_api.DataModel.Project.GroupDM", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.ProjectDM", b =>
                {
                    b.HasOne("marking_api.DataModel.FileSystem.FSFileDM", "EthicsForm")
                        .WithMany()
                        .HasForeignKey("EthicsFormId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EthicsForm");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.UserGradeDM", b =>
                {
                    b.HasOne("marking_api.DataModel.Project.GroupDM", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.Project.UserGroupDM", b =>
                {
                    b.HasOne("marking_api.DataModel.Project.GroupDM", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("marking_api.DataModel.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFileDM", b =>
                {
                    b.Navigation("FileVersions");

                    b.Navigation("FolderFiles");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFileStateDM", b =>
                {
                    b.Navigation("FileVersions");
                });

            modelBuilder.Entity("marking_api.DataModel.FileSystem.FSFolderDM", b =>
                {
                    b.Navigation("ChildFolders");

                    b.Navigation("FolderFiles");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.Role", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("marking_api.DataModel.Identity.User", b =>
                {
                    b.Navigation("UserClaims");

                    b.Navigation("UserLogins");

                    b.Navigation("UserRoles");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
