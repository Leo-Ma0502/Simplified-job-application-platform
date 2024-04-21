﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Data;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Models.Industry", b =>
                {
                    b.Property<int>("IId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IId"));

                    b.Property<string>("IName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IId");

                    b.ToTable("Industries");
                });

            modelBuilder.Entity("WebApi.Models.Job", b =>
                {
                    b.Property<int>("JId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JId"));

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Opening")
                        .HasColumnType("int");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Responsibilities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("WebApi.Models.JobIndustry", b =>
                {
                    b.Property<int>("JId")
                        .HasColumnType("int");

                    b.Property<int>("IId")
                        .HasColumnType("int");

                    b.HasKey("JId", "IId");

                    b.HasIndex("IId");

                    b.ToTable("JobIndustries");
                });

            modelBuilder.Entity("WebApi.Models.JobKeyword", b =>
                {
                    b.Property<int>("JId")
                        .HasColumnType("int");

                    b.Property<int>("KId")
                        .HasColumnType("int");

                    b.HasKey("JId", "KId");

                    b.HasIndex("KId");

                    b.ToTable("JobKeywords");
                });

            modelBuilder.Entity("WebApi.Models.Keyword", b =>
                {
                    b.Property<int>("KId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KId"));

                    b.Property<string>("KName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KId");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.Property<int>("UId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApi.Models.JobIndustry", b =>
                {
                    b.HasOne("WebApi.Models.Industry", "Industry")
                        .WithMany("JobIndustries")
                        .HasForeignKey("IId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Job", "Job")
                        .WithMany("JobIndustries")
                        .HasForeignKey("JId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Industry");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("WebApi.Models.JobKeyword", b =>
                {
                    b.HasOne("WebApi.Models.Job", "Job")
                        .WithMany("JobKeywords")
                        .HasForeignKey("JId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Keyword", "Keyword")
                        .WithMany("JobKeywords")
                        .HasForeignKey("KId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Keyword");
                });

            modelBuilder.Entity("WebApi.Models.Industry", b =>
                {
                    b.Navigation("JobIndustries");
                });

            modelBuilder.Entity("WebApi.Models.Job", b =>
                {
                    b.Navigation("JobIndustries");

                    b.Navigation("JobKeywords");
                });

            modelBuilder.Entity("WebApi.Models.Keyword", b =>
                {
                    b.Navigation("JobKeywords");
                });
#pragma warning restore 612, 618
        }
    }
}
