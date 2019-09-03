﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApp.Data.SqlServer;

namespace ToDoApp.Data.SqlServer.Migrations
{
    [DbContext(typeof(ToDoDbContext))]
    [Migration("20190827153757_TaskRemoved")]
    partial class TaskRemoved
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToDoApp.Domain.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<Guid?>("ParentTaskId");

                    b.Property<Guid?>("ToDoListId");

                    b.HasKey("Id");

                    b.HasIndex("ParentTaskId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDoItem");
                });

            modelBuilder.Entity("ToDoApp.Domain.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("ToDoApp.Domain.ToDoItem", b =>
                {
                    b.HasOne("ToDoApp.Domain.ToDoItem", "ParentTask")
                        .WithMany("TaskItems")
                        .HasForeignKey("ParentTaskId");

                    b.HasOne("ToDoApp.Domain.ToDoList")
                        .WithMany("ToDoItems")
                        .HasForeignKey("ToDoListId");
                });
#pragma warning restore 612, 618
        }
    }
}