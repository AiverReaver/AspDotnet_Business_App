﻿// <auto-generated />
using System;
using BusinessApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusinessApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190531051331_ExtendedBusinessEntityClass")]
    partial class ExtendedBusinessEntityClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("BusinessApp.API.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Description");

                    b.Property<bool>("IsPublishable");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("Landmark");

                    b.Property<string>("Name");

                    b.Property<string>("OfficeNumber");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("BusinessApp.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusinessId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("BusinessApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Gender");

                    b.Property<DateTime>("LastActive");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessApp.API.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusinessId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId")
                        .IsUnique();

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("BusinessApp.API.Models.Business", b =>
                {
                    b.HasOne("BusinessApp.API.Models.User", "User")
                        .WithMany("Businesses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusinessApp.API.Models.Photo", b =>
                {
                    b.HasOne("BusinessApp.API.Models.Business", "Business")
                        .WithMany("Photos")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusinessApp.API.Models.Video", b =>
                {
                    b.HasOne("BusinessApp.API.Models.Business", "Business")
                        .WithOne("Video")
                        .HasForeignKey("BusinessApp.API.Models.Video", "BusinessId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
