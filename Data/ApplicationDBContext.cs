﻿using AltaProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace AltaProject.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<InternalUser> InternalUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<FileImage> File { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Questionnaire> Questionnaire { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<VisitTask> Tasks { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<VisitPlan> VisitPlans { get; set; }

        //set relationship
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<InternalUser>(iu =>
                {
                    iu.HasOne(iu => iu.Staff).WithOne(s => s.InternalUser).HasForeignKey<Staff>(iu => iu.Id).HasConstraintName("FK_InternalUser_Staff");
                    iu.HasOne(iu => iu.Plan).WithOne(vp => vp.RequestorUser).HasForeignKey<VisitPlan>(iu => iu.Id).HasConstraintName("FK_InternalUser_VisitPlan");
                    iu.HasMany(iu => iu.Tasks).WithOne(vt => vt.CreatorUser).HasForeignKey(t => t.CreatorUserId).HasConstraintName("FK_InternalUser_VisitTask");
                    iu.HasOne(iu => iu.Article).WithOne(a => a.CreatorUser).HasForeignKey<Article>(iu => iu.Id).HasConstraintName("FK_InternalUser_Article");
                    iu.HasMany(iu => iu.SendedNotifications).WithOne(n => n.SenderUser).HasForeignKey(n => n.SenderUserId).HasConstraintName("FK_InternalUser_SendedNotification");
                    iu.HasOne(iu => iu.Role).WithMany(r => r.InternalUsers).HasForeignKey(iu => iu.RoleId).HasConstraintName("FK_InternalUser_Role");
                    iu.HasOne(iu => iu.User).WithOne(u => u.InUser).HasForeignKey<InternalUser>(iu => iu.Id).HasConstraintName("FK_InternalUser_User");
                })
                .Entity<Area>(a =>
                {
                    a.HasMany(a => a.Users).WithOne(u => u.Area).HasForeignKey(u => u.AreaId).HasConstraintName("FK_User_Area");
                })
                .Entity<Distributor>(d =>
                {
                    d.HasMany(d => d.Plans).WithOne(d => d.Distributor).HasForeignKey(d => d.DistributorId).HasConstraintName("FK_Distributor_Plan");
                    d.HasOne(d => d.User).WithOne(u => u.Distributor).HasForeignKey<Distributor>(x => x.Id).HasConstraintName("FK_Distributor_User");
                })
                .Entity<Article>(a =>
                {
                    a.HasOne(a => a.File).WithOne(a => a.Article).HasForeignKey<FileImage>(a => a.Id).HasConstraintName("FK_Article_File");
                })
                .Entity<Notification>(n =>
                {
                    n.HasOne(n => n.UserReceiver).WithMany(u => u.ReceivedNotification).HasForeignKey(n => n.UserReceiverId).HasConstraintName("FK_User_ReceiverNotification");
                })
                .Entity<VisitPlan>(vp =>
                {
                    vp.HasOne(vp => vp.Time).WithMany(vp => vp.Plans).HasForeignKey(vp => vp.TimeId).HasConstraintName("FK_VisitPlan_Time");
                    vp.HasMany(vp => vp.Guests).WithMany(g => g.VisitPlans).UsingEntity("VisitPlan_Guest");
                })
                .Entity<VisitTask>(vt =>
                {
                    vt.HasOne(vt => vt.AssigneeStaff).WithMany(vt => vt.Tasks).HasForeignKey(vt => vt.AssigneeStaffId).HasConstraintName("FK_VisitTask_Staff");
                    vt.HasMany(vt => vt.Files).WithOne(vt => vt.Task).HasForeignKey(vt => vt.TaskId).HasConstraintName("FK_VisitTask_FileImage");
                    vt.HasMany(vt => vt.Comments).WithOne(vt => vt.VisitTask).HasForeignKey(vt => vt.TaskId).HasConstraintName("FK_VisitTask_Comment");
                })
                .Entity<Survey>(s =>
                {
                    s.HasOne(s => s.Questionnaire).WithMany(s => s.Surveys).HasForeignKey(s => s.QuestionnaireId).HasConstraintName("FK_Survey_Questionnaire");
                    s.HasOne(s => s.CreatorUser).WithMany(iu => iu.Surveys).HasForeignKey(s => s.CreatorUserId).HasConstraintName("FK_InternalUser_Survey");
                    s.HasMany(s => s.ImplementUsers).WithMany(u => u.Surveys).UsingEntity("User_Survey");
                })
                .Entity<Guest>(g =>
                {
                    g.HasOne(g => g.User).WithOne(u => u.Guest).HasForeignKey<Guest>(g => g.Id).HasConstraintName("FK_Guest_User");
                })
                .Entity<Comment>(c =>
                {
                    c.HasOne(c => c.CommentUser).WithMany(u => u.Comments).HasForeignKey(c => c.CommentUserId).HasConstraintName("FK_User_Comment");
                });
        }
    }
}
