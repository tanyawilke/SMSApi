﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SmsApi.Models;
using System;

namespace SmsApi.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmsApi.Models.CountryModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("country_code");

                    b.Property<int>("mobile_country_code");

                    b.Property<decimal>("sms_price");

                    b.HasKey("id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("SmsApi.Models.SmsModel", b =>
                {
                    b.Property<int>("country_id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date_sent");

                    b.Property<int>("id");

                    b.Property<string>("message");

                    b.Property<int>("number");

                    b.Property<string>("sender");

                    b.HasKey("country_id");

                    b.ToTable("Sms");
                });
#pragma warning restore 612, 618
        }
    }
}
