﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PurchaseGallery.ApiService.Data;

#nullable disable

namespace PurchaseGallery.Api.Migrations
{
    [DbContext(typeof(PurchaseGalleryDbContext))]
    [Migration("20241128142823_UpdateAssetCategoryAndAssetType")]
    partial class UpdateAssetCategoryAndAssetType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AssetPurchaseRequest", b =>
                {
                    b.Property<Guid>("AssetsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PurchaseRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AssetsId", "PurchaseRequestId");

                    b.HasIndex("PurchaseRequestId");

                    b.ToTable("PurchaseRequestAssets", (string)null);
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Assets.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssetCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssetTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssetUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssetCategoryId");

                    b.HasIndex("AssetTypeId");

                    b.ToTable("Asset");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Assets.AssetCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssetCategory");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Assets.AssetType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssetType");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Auth.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Auth.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Countries.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Notifications.Notications", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PurchaseRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseRequestId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.PurchaseRequests.Approval", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ApproverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PurchaseRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("PurchaseRequestId");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.PurchaseRequests.PurchaseRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ApproverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssetIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RequsterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("RequsterId");

                    b.ToTable("PurchaseRequests");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.travellers.Traveller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FromWhereId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ToWhereId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FromWhereId");

                    b.HasIndex("ToWhereId");

                    b.ToTable("Travellers");
                });

            modelBuilder.Entity("PurchaseGallery.ApiService.Models.Auth.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AzureAdId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AssetPurchaseRequest", b =>
                {
                    b.HasOne("PurchaseGallery.Api.Models.Assets.Asset", null)
                        .WithMany()
                        .HasForeignKey("AssetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PurchaseGallery.Api.Models.PurchaseRequests.PurchaseRequest", null)
                        .WithMany()
                        .HasForeignKey("PurchaseRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Assets.Asset", b =>
                {
                    b.HasOne("PurchaseGallery.Api.Models.Assets.AssetCategory", "AssetCategory")
                        .WithMany()
                        .HasForeignKey("AssetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PurchaseGallery.Api.Models.Assets.AssetType", "AssetType")
                        .WithMany()
                        .HasForeignKey("AssetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetCategory");

                    b.Navigation("AssetType");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Auth.UserRole", b =>
                {
                    b.HasOne("PurchaseGallery.Api.Models.Auth.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PurchaseGallery.ApiService.Models.Auth.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Notifications.Notications", b =>
                {
                    b.HasOne("PurchaseGallery.Api.Models.PurchaseRequests.PurchaseRequest", "PurchaseRequest")
                        .WithMany()
                        .HasForeignKey("PurchaseRequestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PurchaseRequest");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.PurchaseRequests.Approval", b =>
                {
                    b.HasOne("PurchaseGallery.ApiService.Models.Auth.User", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PurchaseGallery.Api.Models.PurchaseRequests.PurchaseRequest", "PurchaseRequest")
                        .WithMany()
                        .HasForeignKey("PurchaseRequestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("PurchaseRequest");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.PurchaseRequests.PurchaseRequest", b =>
                {
                    b.HasOne("PurchaseGallery.ApiService.Models.Auth.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApproverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PurchaseGallery.ApiService.Models.Auth.User", "RequesterUser")
                        .WithMany()
                        .HasForeignKey("RequsterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("RequesterUser");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.travellers.Traveller", b =>
                {
                    b.HasOne("PurchaseGallery.Api.Models.Countries.Country", "FromWhere")
                        .WithMany()
                        .HasForeignKey("FromWhereId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PurchaseGallery.Api.Models.Countries.Country", "ToWhere")
                        .WithMany()
                        .HasForeignKey("ToWhereId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FromWhere");

                    b.Navigation("ToWhere");
                });

            modelBuilder.Entity("PurchaseGallery.Api.Models.Auth.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PurchaseGallery.ApiService.Models.Auth.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
