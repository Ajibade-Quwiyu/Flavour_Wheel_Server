﻿// <auto-generated />
using Flavour_Wheel_Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flavour_Wheel_Server.Migrations
{
    [DbContext(typeof(FlavourWheelContext))]
    partial class FlavourWheelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Flavour_Wheel_Server.Model.AdminServer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DrinkCategory")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasscodeKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Spirit1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Spirit2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Spirit3")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Spirit4")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Spirit5")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AdminServers");
                });

            modelBuilder.Entity("Flavour_Wheel_Server.Model.FlavourWheel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OverallRating")
                        .HasColumnType("int");

                    b.Property<int>("Spirit1Flavours")
                        .HasColumnType("int");

                    b.Property<string>("Spirit1Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Spirit1Ratings")
                        .HasColumnType("int");

                    b.Property<int>("Spirit2Flavours")
                        .HasColumnType("int");

                    b.Property<string>("Spirit2Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Spirit2Ratings")
                        .HasColumnType("int");

                    b.Property<int>("Spirit3Flavours")
                        .HasColumnType("int");

                    b.Property<string>("Spirit3Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Spirit3Ratings")
                        .HasColumnType("int");

                    b.Property<int>("Spirit4Flavours")
                        .HasColumnType("int");

                    b.Property<string>("Spirit4Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Spirit4Ratings")
                        .HasColumnType("int");

                    b.Property<int>("Spirit5Flavours")
                        .HasColumnType("int");

                    b.Property<string>("Spirit5Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Spirit5Ratings")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FlavourWheels");
                });
#pragma warning restore 612, 618
        }
    }
}
