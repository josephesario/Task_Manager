using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Models;

public partial class taskManagerDbContext : DbContext
{
    public taskManagerDbContext()
    {
    }

    public taskManagerDbContext(DbContextOptions<taskManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TTask> TTasks { get; set; }

    public virtual DbSet<TTaskStatus> TTaskStatuses { get; set; }

    public virtual DbSet<TUserDetail> TUserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("taskManagerDbContext");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__t_Task__3214EC070389C6A2");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.TTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UserDetails_id");

            entity.HasOne(d => d.Status).WithMany(p => p.TTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status_id");
        });

        modelBuilder.Entity<TTaskStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__t_Task_s__3680B91963894F4B");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.TTaskStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Task_status_UserDetails_id");
        });

        modelBuilder.Entity<TUserDetail>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__t_UserDe__AB6E61659E4A5F67");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
