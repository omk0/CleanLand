﻿// <auto-generated />
using System;
using CleanLand.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanLand.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250315120815_AddForests")]
    partial class AddForests
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanLand.Controllers.Forest.AreaData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ForestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ForestId");

                    b.ToTable("DeforestationDatas");
                });

            modelBuilder.Entity("CleanLand.Controllers.Forest.Forest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("AverageYearHumidity")
                        .HasColumnType("float");

                    b.Property<double>("AverageYearTemperature")
                        .HasColumnType("float");

                    b.Property<double>("CriticalityScore")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FireIncidentsAmount")
                        .HasColumnType("int");

                    b.Property<bool>("IsProtectedByLaw")
                        .HasColumnType("bit");

                    b.Property<string>("NGO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TonsOfSequesteredPotential")
                        .HasColumnType("float");

                    b.Property<double>("TonsOfSequesteredToDate")
                        .HasColumnType("float");

                    b.Property<long?>("TreesAmount")
                        .HasColumnType("bigint");

                    b.Property<double>("XLocation")
                        .HasColumnType("float");

                    b.Property<double>("YLocation")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Forests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AverageYearHumidity = 0.0,
                            AverageYearTemperature = 27.5,
                            CriticalityScore = 8.5,
                            Description = "One of the world's largest and most diverse forests.",
                            FireIncidentsAmount = 50,
                            IsProtectedByLaw = true,
                            NGO = "Amazon Conservation Association",
                            Name = "Amazon Rainforest",
                            TonsOfSequesteredPotential = 5000000000.0,
                            TonsOfSequesteredToDate = 1000000000.0,
                            TreesAmount = 390000000000L,
                            XLocation = -3.4653,
                            YLocation = -62.215899999999998
                        });
                });

            modelBuilder.Entity("CleanLand.Controllers.Forest.TreeSpecie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CommonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEndemic")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInvasive")
                        .HasColumnType("bit");

                    b.Property<string>("ScientificName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxonomicClassification")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TreeSpecies");
                });

            modelBuilder.Entity("CleanLand.Data.Models.LeaseAgreement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("EconomicActivities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TermInYears")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LeaseAgreements");
                });

            modelBuilder.Entity("CleanLand.Data.Models.Lessee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IdentificationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lessees");
                });

            modelBuilder.Entity("CleanLand.Data.Models.Pond", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Basin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CadastralNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CollectedDamages")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CollectedFines")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Coordinates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Depth")
                        .HasColumnType("float");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasHydraulicStructure")
                        .HasColumnType("bit");

                    b.Property<string>("HydraulicStructureOwner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ImposedDamages")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ImposedFines")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDrainable")
                        .HasColumnType("bit");

                    b.Property<string>("Issues")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeaseAgreementId")
                        .HasColumnType("int");

                    b.Property<double>("LeasedArea")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<int>("LesseeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("River")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Settlement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TerritorialCommunity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.Property<double>("WaterLevel")
                        .HasColumnType("float");

                    b.Property<double>("WaterSurfaceArea")
                        .HasColumnType("float");

                    b.Property<int>("WaterUsagePermitId")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("LeaseAgreementId");

                    b.HasIndex("LesseeId");

                    b.HasIndex("WaterUsagePermitId");

                    b.ToTable("Ponds");
                });

            modelBuilder.Entity("CleanLand.Data.Models.WaterUsagePermit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TermInYears")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WaterUsagePermits");
                });

            modelBuilder.Entity("ForestTreeSpecie", b =>
                {
                    b.Property<int>("ForestId")
                        .HasColumnType("int");

                    b.Property<int>("TreeSpeciesId")
                        .HasColumnType("int");

                    b.HasKey("ForestId", "TreeSpeciesId");

                    b.HasIndex("TreeSpeciesId");

                    b.ToTable("ForestTreeSpecie");
                });

            modelBuilder.Entity("CleanLand.Controllers.Forest.AreaData", b =>
                {
                    b.HasOne("CleanLand.Controllers.Forest.Forest", null)
                        .WithMany("AreaDatas")
                        .HasForeignKey("ForestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CleanLand.Data.Models.Pond", b =>
                {
                    b.HasOne("CleanLand.Data.Models.LeaseAgreement", "LeaseAgreement")
                        .WithMany("Ponds")
                        .HasForeignKey("LeaseAgreementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanLand.Data.Models.Lessee", "Lessee")
                        .WithMany("Ponds")
                        .HasForeignKey("LesseeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanLand.Data.Models.WaterUsagePermit", "WaterUsagePermit")
                        .WithMany("Ponds")
                        .HasForeignKey("WaterUsagePermitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaseAgreement");

                    b.Navigation("Lessee");

                    b.Navigation("WaterUsagePermit");
                });

            modelBuilder.Entity("ForestTreeSpecie", b =>
                {
                    b.HasOne("CleanLand.Controllers.Forest.Forest", null)
                        .WithMany()
                        .HasForeignKey("ForestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanLand.Controllers.Forest.TreeSpecie", null)
                        .WithMany()
                        .HasForeignKey("TreeSpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CleanLand.Controllers.Forest.Forest", b =>
                {
                    b.Navigation("AreaDatas");
                });

            modelBuilder.Entity("CleanLand.Data.Models.LeaseAgreement", b =>
                {
                    b.Navigation("Ponds");
                });

            modelBuilder.Entity("CleanLand.Data.Models.Lessee", b =>
                {
                    b.Navigation("Ponds");
                });

            modelBuilder.Entity("CleanLand.Data.Models.WaterUsagePermit", b =>
                {
                    b.Navigation("Ponds");
                });
#pragma warning restore 612, 618
        }
    }
}
