﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZombieParty.Models.Data;

#nullable disable

namespace ZombieParty.Migrations
{
    [DbContext(typeof(ZombiePartyDbContext))]
    [Migration("20230531182416_CreationBD_Zombie_ZombieType")]
    partial class CreationBD_Zombie_ZombieType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ZombieParty.Models.Zombie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<int>("ZombieTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ZombieTypeId");

                    b.ToTable("Zombies");
                });

            modelBuilder.Entity("ZombieParty.Models.ZombieType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ZombieTypes");
                });

            modelBuilder.Entity("ZombieParty.Models.Zombie", b =>
                {
                    b.HasOne("ZombieParty.Models.ZombieType", "ZombieType")
                        .WithMany()
                        .HasForeignKey("ZombieTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ZombieType");
                });
#pragma warning restore 612, 618
        }
    }
}
