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
            entity.HasKey(e => e.Id).HasName("PK__t_Task__3214EC07BCEA58F5");
        });

        modelBuilder.Entity<TUserDetail>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__t_UserDe__AB6E61653E3264A4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
