﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220404145802_BannerTable2")]
    partial class BannerTable2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Articles.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleCategoryId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Avatar")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("AvatarAlt")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AvatarTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(155)
                        .HasColumnType("nvarchar(155)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("KeyWords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdateUserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegisterUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleCategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Entities.Articles.ArticleCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Avatar")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("AvatarAlt")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AvatarTitle")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .HasMaxLength(155)
                        .HasColumnType("nvarchar(155)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdateUserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegisterUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("ArticleCategories");
                });

            modelBuilder.Entity("Entities.Articles.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Entities.Media.Banner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link8")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link9")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title1")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title2")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title3")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title4")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title5")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title6")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title7")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title8")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title9")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("Entities.Media.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarAlt")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("Entities.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Avatar")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("AvatarAlt")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AvatarTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("KeyWords")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdateUserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegisterUserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entities.Products.ProductArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductArticles");
                });

            modelBuilder.Entity("Entities.Products.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int?>("SortNum")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Entities.Products.ProductPharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPharmacies");
                });

            modelBuilder.Entity("Entities.pharmacies.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("DrName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PharmacyCode")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Entities.Articles.Article", b =>
                {
                    b.HasOne("Entities.Articles.ArticleCategory", "ArticleCategory")
                        .WithMany("Articles")
                        .HasForeignKey("ArticleCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ArticleCategory");
                });

            modelBuilder.Entity("Entities.Articles.Comment", b =>
                {
                    b.HasOne("Entities.Articles.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("Entities.Products.Product", b =>
                {
                    b.HasOne("Entities.Products.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Entities.Products.ProductArticle", b =>
                {
                    b.HasOne("Entities.Articles.Article", "Article")
                        .WithMany("ProductArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Entities.Products.Product", "Product")
                        .WithMany("ProductArticles")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Products.ProductPharmacy", b =>
                {
                    b.HasOne("Entities.pharmacies.Pharmacy", "Pharmacy")
                        .WithMany("ProductPharmacies")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Entities.Products.Product", "Product")
                        .WithMany("ProductPharmacies")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pharmacy");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Articles.Article", b =>
                {
                    b.Navigation("ProductArticles");
                });

            modelBuilder.Entity("Entities.Articles.ArticleCategory", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Entities.Products.Product", b =>
                {
                    b.Navigation("ProductArticles");

                    b.Navigation("ProductPharmacies");
                });

            modelBuilder.Entity("Entities.Products.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Entities.pharmacies.Pharmacy", b =>
                {
                    b.Navigation("ProductPharmacies");
                });
#pragma warning restore 612, 618
        }
    }
}
