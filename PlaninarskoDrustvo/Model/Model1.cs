namespace PlaninarskoDrustvo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<_event> events { get; set; }
        public virtual DbSet<gallery> galleries { get; set; }
        public virtual DbSet<image> images { get; set; }
        public virtual DbSet<member> members { get; set; }
        public virtual DbSet<membership_fee> membership_fee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.name)
                .IsUnicode(false);

            //modelBuilder.Entity<_event>()
            //    .Property(e => e.location)
            //    .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.galleries)
                .WithRequired(e => e._event)
                .HasForeignKey(e => e.event_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<gallery>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<gallery>()
                .HasMany(e => e.images)
                .WithRequired(e => e.gallery)
                .HasForeignKey(e => e.gallery_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<image>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .HasOptional(e => e.account)
                .WithRequired(e => e.member);

            modelBuilder.Entity<member>()
                .HasMany(e => e.membership_fee)
                .WithRequired(e => e.member)
                .HasForeignKey(e => e.member_id)
                .WillCascadeOnDelete(false);
        }
    }
}
