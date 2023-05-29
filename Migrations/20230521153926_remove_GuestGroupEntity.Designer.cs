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
    [Migration("20230521153926_remove_GuestGroupEntity")]
    partial class remove_GuestGroupEntity
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

                    b.Property<int>("CommentUserId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CommentUserId");

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
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("AltaProject.Entity.InternalUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("InternalUsers");
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

                    b.Property<int>("UserReceiverId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SenderUserId");

                    b.HasIndex("UserReceiverId");

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

                    b.Property<int?>("CreatorUserId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

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

            modelBuilder.Entity("AltaProject.Entity.User", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Users");
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AssigneeStaffId")
                        .HasColumnType("integer");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float?>("Rating")
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

                    b.HasIndex("AssigneeStaffId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("User_Survey", b =>
                {
                    b.Property<int>("ImplementUsersId")
                        .HasColumnType("integer");

                    b.Property<int>("SurveysId")
                        .HasColumnType("integer");

                    b.HasKey("ImplementUsersId", "SurveysId");

                    b.HasIndex("SurveysId");

                    b.ToTable("User_Survey");
                });

            modelBuilder.Entity("VisitPlan_Guest", b =>
                {
                    b.Property<int>("GuestsId")
                        .HasColumnType("integer");

                    b.Property<int>("VisitPlansId")
                        .HasColumnType("integer");

                    b.HasKey("GuestsId", "VisitPlansId");

                    b.HasIndex("VisitPlansId");

                    b.ToTable("VisitPlan_Guest");
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
                    b.HasOne("AltaProject.Entity.User", "CommentUser")
                        .WithMany("Comments")
                        .HasForeignKey("CommentUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Comment");

                    b.HasOne("AltaProject.Entity.VisitTask", "VisitTask")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_VisitTask_Comment");

                    b.Navigation("CommentUser");

                    b.Navigation("VisitTask");
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
                    b.HasOne("AltaProject.Entity.User", "User")
                        .WithOne("Guest")
                        .HasForeignKey("AltaProject.Entity.Guest", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Guest_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AltaProject.Entity.InternalUser", b =>
                {
                    b.HasOne("AltaProject.Entity.User", "User")
                        .WithOne("InternalUser")
                        .HasForeignKey("AltaProject.Entity.InternalUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_User");

                    b.HasOne("AltaProject.Entity.Role", "Role")
                        .WithMany("InternalUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_Role");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AltaProject.Entity.Notification", b =>
                {
                    b.HasOne("AltaProject.Entity.InternalUser", "SenderUser")
                        .WithMany("SendedNotifications")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_SendedNotification");

                    b.HasOne("AltaProject.Entity.User", "UserReceiver")
                        .WithMany("ReceivedNotification")
                        .HasForeignKey("UserReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_ReceiverNotification");

                    b.Navigation("SenderUser");

                    b.Navigation("UserReceiver");
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

            modelBuilder.Entity("AltaProject.Entity.Staff", b =>
                {
                    b.HasOne("AltaProject.Entity.Area", "Area")
                        .WithMany("Staffs")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Area_Staff");

                    b.HasOne("AltaProject.Entity.InternalUser", "InternalUser")
                        .WithOne("Staff")
                        .HasForeignKey("AltaProject.Entity.Staff", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_Staff");

                    b.Navigation("Area");

                    b.Navigation("InternalUser");
                });

            modelBuilder.Entity("AltaProject.Entity.Survey", b =>
                {
                    b.HasOne("AltaProject.Entity.InternalUser", "CreatorUser")
                        .WithMany("Surveys")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_Survey");

                    b.Navigation("CreatorUser");
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
                        .WithMany("Tasks")
                        .HasForeignKey("AssigneeStaffId")
                        .HasConstraintName("FK_VisitTask_Staff");

                    b.HasOne("AltaProject.Entity.InternalUser", "CreatorUser")
                        .WithMany("Tasks")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_InternalUser_VisitTask");

                    b.Navigation("AssigneeStaff");

                    b.Navigation("CreatorUser");
                });

            modelBuilder.Entity("User_Survey", b =>
                {
                    b.HasOne("AltaProject.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("ImplementUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltaProject.Entity.Survey", null)
                        .WithMany()
                        .HasForeignKey("SurveysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VisitPlan_Guest", b =>
                {
                    b.HasOne("AltaProject.Entity.Guest", null)
                        .WithMany()
                        .HasForeignKey("GuestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AltaProject.Entity.VisitPlan", null)
                        .WithMany()
                        .HasForeignKey("VisitPlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("AltaProject.Entity.InternalUser", b =>
                {
                    b.Navigation("Article")
                        .IsRequired();

                    b.Navigation("Plan")
                        .IsRequired();

                    b.Navigation("SendedNotifications");

                    b.Navigation("Staff")
                        .IsRequired();

                    b.Navigation("Surveys");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("AltaProject.Entity.Role", b =>
                {
                    b.Navigation("InternalUsers");
                });

            modelBuilder.Entity("AltaProject.Entity.Staff", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("AltaProject.Entity.Survey", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("AltaProject.Entity.Time", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("AltaProject.Entity.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Guest")
                        .IsRequired();

                    b.Navigation("InternalUser")
                        .IsRequired();

                    b.Navigation("ReceivedNotification");
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
