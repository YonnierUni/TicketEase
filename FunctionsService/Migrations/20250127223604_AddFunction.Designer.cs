﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketEase.Service.Functions.DbContexts;

#nullable disable

namespace TicketEase.Service.Functions.Migrations
{
    [DbContext(typeof(FunctionsDbContext))]
    [Migration("20250127223604_AddFunction")]
    partial class AddFunction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketEase.Service.Functions.Entities.MovieFunction", b =>
                {
                    b.Property<Guid>("MovieFunctionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("MovieFunctionId");

                    b.ToTable("MovieFunctions");

                    b.HasData(
                        new
                        {
                            MovieFunctionId = new Guid("2a943434-f25e-4233-97b8-0135de632951"),
                            AvailableSeats = 100,
                            EndTime = new DateTime(2025, 1, 25, 21, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                            Room = "Room 1",
                            StartTime = new DateTime(2025, 1, 25, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalSeats = 100
                        },
                        new
                        {
                            MovieFunctionId = new Guid("44b2a7b7-f0ff-4a74-8bbf-856c19abfdb9"),
                            AvailableSeats = 150,
                            EndTime = new DateTime(2025, 1, 26, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"),
                            Room = "Room 2",
                            StartTime = new DateTime(2025, 1, 26, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalSeats = 150
                        },
                        new
                        {
                            MovieFunctionId = new Guid("3b0d79ff-a7e0-4d40-b594-44a49f380634"),
                            AvailableSeats = 100,
                            EndTime = new DateTime(2025, 1, 25, 13, 30, 0, 0, DateTimeKind.Unspecified),
                            MovieId = new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                            Room = "Room 1",
                            StartTime = new DateTime(2025, 1, 25, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalSeats = 100
                        },
                        new
                        {
                            MovieFunctionId = new Guid("57c843b9-b8b0-493c-bd99-6497aa1eec1a"),
                            AvailableSeats = 100,
                            EndTime = new DateTime(2025, 1, 25, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            MovieId = new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                            Room = "Room 1",
                            StartTime = new DateTime(2025, 1, 25, 13, 30, 0, 0, DateTimeKind.Unspecified),
                            TotalSeats = 100
                        },
                        new
                        {
                            MovieFunctionId = new Guid("6e8b819f-76c2-402f-aaba-3fba1a79a06d"),
                            AvailableSeats = 100,
                            EndTime = new DateTime(2025, 1, 25, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            MovieId = new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                            Room = "Room 1",
                            StartTime = new DateTime(2025, 1, 25, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            TotalSeats = 100
                        },
                        new
                        {
                            MovieFunctionId = new Guid("9a271081-4581-43b9-b97d-1419db615f43"),
                            AvailableSeats = 100,
                            EndTime = new DateTime(2025, 1, 25, 23, 45, 0, 0, DateTimeKind.Unspecified),
                            MovieId = new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                            Room = "Room 1",
                            StartTime = new DateTime(2025, 1, 25, 21, 45, 0, 0, DateTimeKind.Unspecified),
                            TotalSeats = 100
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
