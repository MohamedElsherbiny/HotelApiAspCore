﻿// <auto-generated />
using System;
using HotelApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelApp.Api.Migrations.HotelDb
{
    [DbContext(typeof(HotelDbContext))]
    partial class HotelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("HotelApp.Api.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Coordinates");

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<int>("Stars");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("HotelApp.Api.Models.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("HotelId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("PointOfInterests");
                });

            modelBuilder.Entity("HotelApp.Api.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckInDate");

                    b.Property<DateTime>("CheckOutDate");

                    b.Property<string>("ConfirmKey");

                    b.Property<string>("EmployeeName");

                    b.Property<string>("GustName");

                    b.Property<int>("HotelId");

                    b.Property<int>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelApp.Api.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HotelId");

                    b.Property<bool>("IsAvailable");

                    b.Property<int>("Rate");

                    b.Property<int>("RoomType");

                    b.HasKey("RoomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelApp.Api.Models.PointOfInterest", b =>
                {
                    b.HasOne("HotelApp.Api.Models.Hotel", "Hotel")
                        .WithMany("PointOfInterests")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelApp.Api.Models.Reservation", b =>
                {
                    b.HasOne("HotelApp.Api.Models.Hotel", "Hotel")
                        .WithMany("Reservations")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HotelApp.Api.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelApp.Api.Models.Room", b =>
                {
                    b.HasOne("HotelApp.Api.Models.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
