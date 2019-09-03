﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApp.Data.SqlServer;

namespace ToDoApp.Data.SqlServer.Migrations
{
    [DbContext(typeof(ToDoDbContext))]
    partial class ToDoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToDoApp.Domain.Subtask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<Guid>("TaskItemId");

                    b.HasKey("Id");

                    b.HasIndex("TaskItemId");

                    b.ToTable("Subtask");
                });

            modelBuilder.Entity("ToDoApp.Domain.TaskItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<Guid?>("ParentListId");

                    b.HasKey("Id");

                    b.HasIndex("ParentListId");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("ToDoApp.Domain.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("ToDoApp.Domain.Subtask", b =>
                {
                    b.HasOne("ToDoApp.Domain.TaskItem", "ParentTask")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ToDoApp.Domain.TaskItem", b =>
                {
                    b.HasOne("ToDoApp.Domain.ToDoList", "ParentList")
                        .WithMany("TaskItems")
                        .HasForeignKey("ParentListId");
                });
#pragma warning restore 612, 618
        }
    }
}
