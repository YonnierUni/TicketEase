﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketEase.Service.Movies.DbContexts;

#nullable disable

namespace TicketEase.Service.Movies.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20250121063600_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketEase.Service.Movies.Entities.Movie", b =>
                {
                    b.Property<Guid>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cast")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            MovieId = new Guid("7a573bf8-d473-4291-b221-b39b7ca415c9"),
                            Cast = "Robert Downey Jr., Chris Evans, Scarlett Johansson",
                            Director = "Anthony Russo, Joe Russo",
                            Genre = "Action",
                            PosterImage = "endgame.jpg",
                            Title = "Avengers: Endgame",
                            Year = 2019
                        },
                        new
                        {
                            MovieId = new Guid("d12f5f9e-05a3-4742-8ecf-174c4ae57527"),
                            Cast = "Tom Holland, Zendaya, Benedict Cumberbatch",
                            Director = "Jon Watts",
                            Genre = "Action, Sci-Fi",
                            PosterImage = "spiderman_no_way_home.jpg",
                            Title = "Spider-Man: No Way Home",
                            Year = 2021
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
