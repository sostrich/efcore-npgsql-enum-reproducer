﻿// <auto-generated />
using System;
using EnumBugReproducer.DAL;
using EnumBugReproducer.Models.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EfCoreNpgsqlPublicEnumReproducer.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("myschema")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "my_enum", new[] { "value_one", "value2" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EnumBugReproducer.Models.DAL.DbEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<MyEnum>("my_discriminator")
                        .HasColumnType("my_enum");

                    b.HasKey("Id");

                    b.ToTable("entities", "myschema");

                    b.HasDiscriminator<MyEnum>("my_discriminator");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EnumBugReproducer.Models.DAL.DbEntitySubclassOne", b =>
                {
                    b.HasBaseType("EnumBugReproducer.Models.DAL.DbEntity");

                    b.HasDiscriminator().HasValue(MyEnum.ValueOne);
                });

            modelBuilder.Entity("EnumBugReproducer.Models.DAL.DbEntitySubclassTwo", b =>
                {
                    b.HasBaseType("EnumBugReproducer.Models.DAL.DbEntity");

                    b.HasDiscriminator().HasValue(MyEnum.Value2);
                });
#pragma warning restore 612, 618
        }
    }
}
