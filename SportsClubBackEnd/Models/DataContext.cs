using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SportsClubBackEnd.Models;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=SportClub;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B4984016394");

            entity.ToTable("Schedule");

            entity.HasOne(d => d.Service).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Schedule__Servic__403A8C7D");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK__Schedule__Traine__4222D4EF");

            entity.HasOne(d => d.Visitor).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.VisitorId)
                .HasConstraintName("FK__Schedule__Visito__412EB0B6");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00A6B4B97F2");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceName).HasMaxLength(255);
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__Trainers__366A1A7CAE6E0578");

            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId).HasName("PK__Visitors__B121AF8893F8CBAB");

            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
