namespace Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<COM_CONTENT> COM_CONTENT { get; set; }
        public virtual DbSet<COM_DAILYS> COM_DAILYS { get; set; }
        public virtual DbSet<COM_UPLOAD> COM_UPLOAD { get; set; }
        public virtual DbSet<COM_WORKATTENDANCE> COM_WORKATTENDANCE { get; set; }
        public virtual DbSet<MAIL_ATTACHMENT> MAIL_ATTACHMENT { get; set; }
        public virtual DbSet<MAIL_INBOX> MAIL_INBOX { get; set; }
        public virtual DbSet<MAIL_OUTBOX> MAIL_OUTBOX { get; set; }
        public virtual DbSet<PRO_PROJECT_FILES> PRO_PROJECT_FILES { get; set; }
        public virtual DbSet<PRO_PROJECT_MESSAGE> PRO_PROJECT_MESSAGE { get; set; }
        public virtual DbSet<PRO_PROJECT_STAGES> PRO_PROJECT_STAGES { get; set; }
        public virtual DbSet<PRO_PROJECT_TEAMS> PRO_PROJECT_TEAMS { get; set; }
        public virtual DbSet<PRO_PROJECTS> PRO_PROJECTS { get; set; }
        public virtual DbSet<SYS_BUSSINESSCUSTOMER> SYS_BUSSINESSCUSTOMER { get; set; }
        public virtual DbSet<SYS_CHATMESSAGE> SYS_CHATMESSAGE { get; set; }
        public virtual DbSet<SYS_CODE> SYS_CODE { get; set; }
        public virtual DbSet<SYS_CODE_AREA> SYS_CODE_AREA { get; set; }
        public virtual DbSet<SYS_DEPARTMENT> SYS_DEPARTMENT { get; set; }
        public virtual DbSet<SYS_LOG> SYS_LOG { get; set; }
        public virtual DbSet<SYS_MODULE> SYS_MODULE { get; set; }
        public virtual DbSet<SYS_PERMISSION> SYS_PERMISSION { get; set; }
        public virtual DbSet<SYS_POST> SYS_POST { get; set; }
        public virtual DbSet<SYS_POST_USER> SYS_POST_USER { get; set; }
        public virtual DbSet<SYS_ROLE> SYS_ROLE { get; set; }
        public virtual DbSet<SYS_ROLE_PERMISSION> SYS_ROLE_PERMISSION { get; set; }
        public virtual DbSet<SYS_SYSTEM> SYS_SYSTEM { get; set; }
        public virtual DbSet<SYS_USER> SYS_USER { get; set; }
        public virtual DbSet<SYS_USER_DEPARTMENT> SYS_USER_DEPARTMENT { get; set; }
        public virtual DbSet<SYS_USER_ONLINE> SYS_USER_ONLINE { get; set; }
        public virtual DbSet<SYS_USER_PERMISSION> SYS_USER_PERMISSION { get; set; }
        public virtual DbSet<SYS_USER_ROLE> SYS_USER_ROLE { get; set; }
        public virtual DbSet<SYS_USERINFO> SYS_USERINFO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MAIL_OUTBOX>()
                .HasMany(e => e.MAIL_ATTACHMENT)
                .WithRequired(e => e.MAIL_OUTBOX)
                .HasForeignKey(e => e.FK_MailID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MAIL_OUTBOX>()
                .HasMany(e => e.MAIL_INBOX)
                .WithRequired(e => e.MAIL_OUTBOX)
                .HasForeignKey(e => e.FK_MailID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRO_PROJECT_STAGES>()
                .HasMany(e => e.PRO_PROJECT_TEAMS)
                .WithRequired(e => e.PRO_PROJECT_STAGES)
                .HasForeignKey(e => e.FK_StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRO_PROJECTS>()
                .HasMany(e => e.PRO_PROJECT_MESSAGE)
                .WithRequired(e => e.PRO_PROJECTS)
                .HasForeignKey(e => e.FK_ProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRO_PROJECTS>()
                .HasMany(e => e.PRO_PROJECT_STAGES)
                .WithRequired(e => e.PRO_PROJECTS)
                .HasForeignKey(e => e.FK_ProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_CODE_AREA>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_CODE_AREA>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<SYS_DEPARTMENT>()
                .HasMany(e => e.SYS_POST)
                .WithRequired(e => e.SYS_DEPARTMENT)
                .HasForeignKey(e => e.FK_DEPARTID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_MODULE>()
                .HasMany(e => e.SYS_PERMISSION)
                .WithRequired(e => e.SYS_MODULE)
                .HasForeignKey(e => e.MODULEID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_PERMISSION>()
                .HasMany(e => e.SYS_ROLE_PERMISSION)
                .WithRequired(e => e.SYS_PERMISSION)
                .HasForeignKey(e => e.PERMISSIONID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_PERMISSION>()
                .HasMany(e => e.SYS_USER_PERMISSION)
                .WithRequired(e => e.SYS_PERMISSION)
                .HasForeignKey(e => e.FK_PERMISSIONID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_ROLE>()
                .HasMany(e => e.SYS_ROLE_PERMISSION)
                .WithRequired(e => e.SYS_ROLE)
                .HasForeignKey(e => e.ROLEID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_ROLE>()
                .HasMany(e => e.SYS_USER_ROLE)
                .WithRequired(e => e.SYS_ROLE)
                .HasForeignKey(e => e.FK_ROLEID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_SYSTEM>()
                .HasMany(e => e.SYS_MODULE)
                .WithRequired(e => e.SYS_SYSTEM)
                .HasForeignKey(e => e.FK_BELONGSYSTEM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_USER>()
                .HasMany(e => e.SYS_USER_ONLINE)
                .WithRequired(e => e.SYS_USER)
                .HasForeignKey(e => e.FK_UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_USER>()
                .HasMany(e => e.SYS_USER_PERMISSION)
                .WithRequired(e => e.SYS_USER)
                .HasForeignKey(e => e.FK_USERID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_USER>()
                .HasMany(e => e.SYS_USER_ROLE)
                .WithRequired(e => e.SYS_USER)
                .HasForeignKey(e => e.FK_USERID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_USER>()
                .HasMany(e => e.SYS_USERINFO)
                .WithRequired(e => e.SYS_USER)
                .HasForeignKey(e => e.USERID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SYS_USER_DEPARTMENT>()
                .Property(e => e.DEPARTMENT_ID)
                .IsUnicode(false);
        }
    }
}
