﻿// <auto-generated />
using System;
using DataLayer.CustomData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20230819144850_cmsTable")]
    partial class cmsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataLayer.Model.CMSModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsEdit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Page")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CMS");
                });

            modelBuilder.Entity("DataLayer.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("longtext");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ForgetPasswordGuid")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("GuidValid")
                        .HasColumnType("datetime");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime");

                    b.Property<int>("LoginAttempt")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
