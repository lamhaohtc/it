using System.Data.Entity;


namespace Managing_Teacher_Work.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {
        }

        public virtual DbSet<CalendarWorking> CalendarWorking { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Credentials> Credentials { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<GroupUser> GroupUser { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Science> Science { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TypeCalendar> TypeCalendar { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Work> Work { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<TeacherActivity> TeacherActivities { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AppDbContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Student)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Credentials>()
                .Property(e => e.UserGroupID)
                .IsUnicode(false);

            modelBuilder.Entity<Credentials>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<GroupUser>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<GroupUser>()
                .Property(e => e.CodeRole)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Science>()
                .HasMany(e => e.Class)
                .WithRequired(e => e.Science)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Science>()
                .HasMany(e => e.Teacher)
                .WithRequired(e => e.Science)
                .HasForeignKey(e => e.SicenceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Class)
                .WithRequired(e => e.Teacher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeCalendar>()
                .HasMany(e => e.CalendarWorking)
                .WithRequired(e => e.TypeCalendar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.GroupID)
                .IsUnicode(false);

            modelBuilder.Entity<Work>()
                .HasMany(e => e.CalendarWorking)
                .WithRequired(e => e.Work)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .Property(t => t.RoleId);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Transactions)
                .WithRequired(t => t.Teacher)
                .HasForeignKey(t => t.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.TeacherActivities)
                .WithRequired(t => t.Teacher)
                .HasForeignKey(t => t.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasMany(t => t.TeacherActivities)
                .WithRequired(t => t.Activity)
                .HasForeignKey(t => t.ActivityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Users)
                .WithRequired(t => t.Teacher)
                .HasForeignKey(t => t.TeacherId)
                .WillCascadeOnDelete(false);

        }

    }
}
