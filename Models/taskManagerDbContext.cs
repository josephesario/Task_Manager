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
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json") // Adjust the file name if necessary
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("taskManagerDbContext"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TTask>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__t_Task__737584F7A1712A6A");
        });

        modelBuilder.Entity<TTaskStatus>(entity =>
        {
            entity.HasKey(e => e.Status).HasName("PK__t_Task_s__A858923D12BE16C4");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.TTaskStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Task_status_UserDetails_id");
        });

        modelBuilder.Entity<TUserDetail>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__t_UserDe__AB6E616561E5571E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
