﻿// <auto-generated />
using System;
using AltaProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AltaProject.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230425162922_fixed-InternalUser-Staff")]
    partial class fixedInternalUserStaff
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AltaProject.Entity.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("AltaProject.Entity.Article", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HyperText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("AltaProject.Entity.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AltaProject.Entity.Distributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("AreaId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Distributors");
                });

            modelBuilder.Entity("AltaProject.Entity.FileImage", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("AltaProject.Entity.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int?>("GuestGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GuestGroupId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("AltaProject.Entity.GuestGroup", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("GuestGroups");
                });

            modelBuilder.Entity("AltaProject.Entity.InternalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AltaProject.Entity.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SenderUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("AltaProject.Entity.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SurveyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("AltaProject.Entity.Receiver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GuestId")
                        .HasColumnType("integer");

                    b.Property<int>("NotificationId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GuestId")
                        .IsUnique();

                    b.HasIndex("NotificationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Receivers");
                });

            modelBuilder.Entity("AltaProject.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AltaProject.Entity.Staff", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("AreaId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActived")
                        .HasColumnType("boolean");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("AltaProject.Entity.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ImplementUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ImplementUserId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("AltaProject.Entity.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("AltaProject.Entity.VisitPlan", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DistributorId")
                        .HasColumnType("integer");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TimeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.HasIndex("TimeId");

                    b.ToTable("VisitPlans");
                });

            modelBuilder.Entity("AltaProject.Entity.VisitTask", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("AssigneeStaffId")
                        .HasColumnType("integer");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeStaffId")
                        .IsUnique();

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("AltaProject.Entity.Article", b =>
                {
                    b.HasOne("AltaProject.Entity.InternalUser", "CreatorUser")
                        .WithOne("Article")
                        .HasForeignKey("AltaProject.Entity.Article", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_Article");

                    b.Navigation("CreatorUser");
                });

            modelBuilder.Entity("AltaProject.Entity.Comment", b =>
                {
                    b.HasOne("AltaProject.Entity.VisitTask", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_VisitTask_Comment");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("AltaProject.Entity.Distributor", b =>
                {
                    b.HasOne("AltaProject.Entity.Area", "Area")
                        .WithMany("Distributors")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Area_Distributor");

                    b.Navigation("Area");
                });

            modelBuilder.Entity("AltaProject.Entity.FileImage", b =>
                {
                    b.HasOne("AltaProject.Entity.Article", "Article")
                        .WithOne("File")
                        .HasForeignKey("AltaProject.Entity.FileImage", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Article_File");

                    b.HasOne("AltaProject.Entity.VisitTask", "Task")
                        .WithMany("Files")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK_VisitTask_FileImage");

                    b.Navigation("Article");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("AltaProject.Entity.Guest", b =>
                {
                    b.HasOne("AltaProject.Entity.GuestGroup", "GuestGroup")
                        .WithMany("Guests")
                        .HasForeignKey("GuestGroupId");

                    b.Navigation("GuestGroup");
                });

            modelBuilder.Entity("AltaProject.Entity.GuestGroup", b =>
                {
                    b.HasOne("AltaProject.Entity.VisitPlan", "Plan")
                        .WithOne("GuestGroup")
                        .HasForeignKey("AltaProject.Entity.GuestGroup", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_VisitPlan_GuestGroup");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("AltaProject.Entity.InternalUser", b =>
                {
                    b.HasOne("AltaProject.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AltaProject.Entity.Notification", b =>
                {
                    b.HasOne("AltaProject.Entity.InternalUser", "SenderUser")
                        .WithMany("Notifications")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_Notification");

                    b.Navigation("SenderUser");
                });

            modelBuilder.Entity("AltaProject.Entity.Question", b =>
                {
                    b.HasOne("AltaProject.Entity.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Survey_Question");

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("AltaProject.Entity.Receiver", b =>
                {
                    b.HasOne("AltaProject.Entity.Guest", "Guest")
                        .WithOne("Receiver")
                        .HasForeignKey("AltaProject.Entity.Receiver", "GuestId")
                        .HasConstraintName("FK_Guest_Receiver");

                    b.HasOne("AltaProject.Entity.Notification", "Notification")
                        .WithMany("Receivers")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Notification_Receiver");

                    b.HasOne("AltaProject.Entity.InternalUser", "User")
                        .WithOne("Receiver")
                        .HasForeignKey("AltaProject.Entity.Receiver", "UserId")
                        .HasConstraintName("FK_InternalUser_Receiver");

                    b.Navigation("Guest");

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AltaProject.Entity.Staff", b =>
                {
                    b.HasOne("AltaProject.Entity.Area", "Area")
                        .WithMany("Staffs")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Area_Staff");

                    b.HasOne("AltaProject.Entity.InternalUser", "User")
                        .WithOne("Staff")
                        .HasForeignKey("AltaProject.Entity.Staff", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_Staff");

                    b.Navigation("Area");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AltaProject.Entity.Survey", b =>
                {
                    b.HasOne("AltaProject.Entity.InternalUser", "ImplementUser")
                        .WithMany("Surveys")
                        .HasForeignKey("ImplementUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImplementUser");
                });

            modelBuilder.Entity("AltaProject.Entity.VisitPlan", b =>
                {
                    b.HasOne("AltaProject.Entity.Distributor", "Distributor")
                        .WithMany("Plans")
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Distributor_Plan");

                    b.HasOne("AltaProject.Entity.InternalUser", "RequestorUser")
                        .WithOne("Plan")
                        .HasForeignKey("AltaProject.Entity.VisitPlan", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_VisitPlan");

                    b.HasOne("AltaProject.Entity.Time", "Time")
                        .WithMany("Plans")
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_VisitPlan_Time");

                    b.Navigation("Distributor");

                    b.Navigation("RequestorUser");

                    b.Navigation("Time");
                });

            modelBuilder.Entity("AltaProject.Entity.VisitTask", b =>
                {
                    b.HasOne("AltaProject.Entity.Staff", "AssigneeStaff")
                        .WithOne("Task")
                        .HasForeignKey("AltaProject.Entity.VisitTask", "AssigneeStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_VisitTask_Staff");

                    b.HasOne("AltaProject.Entity.InternalUser", "CreatorUser")
                        .WithOne("Task")
                        .HasForeignKey("AltaProject.Entity.VisitTask", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_VisitTask");

                    b.Navigation("AssigneeStaff");

                    b.Navigation("CreatorUser");
                });

            modelBuilder.Entity("AltaProject.Entity.Area", b =>
                {
                    b.Navigation("Distributors");

                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("AltaProject.Entity.Article", b =>
                {
                    b.Navigation("File")
                        .IsRequired();
                });

            modelBuilder.Entity("AltaProject.Entity.Distributor", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("AltaProject.Entity.Guest", b =>
                {
                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("AltaProject.Entity.GuestGroup", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("AltaProject.Entity.InternalUser", b =>
                {
                    b.Navigation("Article")
                        .IsRequired();

                    b.Navigation("Notifications");

                    b.Navigation("Plan")
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Staff")
                        .IsRequired();

                    b.Navigation("Surveys");

                    b.Navigation("Task")
                        .IsRequired();
                });

            modelBuilder.Entity("AltaProject.Entity.Notification", b =>
                {
                    b.Navigation("Receivers");
                });

            modelBuilder.Entity("AltaProject.Entity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("AltaProject.Entity.Staff", b =>
                {
                    b.Navigation("Task")
                        .IsRequired();
                });

            modelBuilder.Entity("AltaProject.Entity.Survey", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("AltaProject.Entity.Time", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("AltaProject.Entity.VisitPlan", b =>
                {
                    b.Navigation("GuestGroup")
                        .IsRequired();
                });

            modelBuilder.Entity("AltaProject.Entity.VisitTask", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
