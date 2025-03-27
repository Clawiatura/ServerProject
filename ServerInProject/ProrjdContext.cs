using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using ServerInProject.Models;

namespace ServerInProject;

public partial class ProrjdContext : DbContext
{
    public ProrjdContext()
    {
    }

    public ProrjdContext(DbContextOptions<ProrjdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Бронирование> Бронированиеs { get; set; }

    public virtual DbSet<Города> Городаs { get; set; }

    public virtual DbSet<Клиент> Клиентs { get; set; }

    public virtual DbSet<Перевозчик> Перевозчикs { get; set; }

    public virtual DbSet<СвободныеМаршруты> СвободныеМаршрутыs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=193.104.57.148;database=prorjd;user=debianrjd;password=toor", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.11-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.ОплатаId).HasName("PRIMARY");

            entity.HasIndex(e => e.IdКлиента, "ID_Клиента");

            entity.HasIndex(e => e.БилетId, "БилетId");

            entity.Property(e => e.ОплатаId).HasColumnType("int(11)");
            entity.Property(e => e.IdКлиента)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Клиента");
            entity.Property(e => e.БилетId).HasColumnType("int(11)");
            entity.Property(e => e.ДатаОплаты).HasColumnType("datetime");
            entity.Property(e => e.Статус).HasColumnType("enum('Pending','Completed','Failed')");
            entity.Property(e => e.Сумма).HasPrecision(10, 2);

            entity.HasOne(d => d.IdКлиентаNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdКлиента)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Payments_ibfk_2");

            entity.HasOne(d => d.Билет).WithMany(p => p.Payments)
                .HasForeignKey(d => d.БилетId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Payments_ibfk_1");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.БилетId).HasName("PRIMARY");

            entity.HasIndex(e => e.IdКлиента, "ID_Клиента");

            entity.Property(e => e.БилетId)
                .HasColumnType("int(11)")
                .HasColumnName("БилетID");
            entity.Property(e => e.IdКлиента)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Клиента");
            entity.Property(e => e.ГородОтправления)
                .HasMaxLength(255)
                .HasColumnName("Город_отправления");
            entity.Property(e => e.ГородПрибытия)
                .HasMaxLength(255)
                .HasColumnName("Город_Прибытия");
            entity.Property(e => e.НомерМеста)
                .HasColumnType("int(11)")
                .HasColumnName("Номер_Места");

            entity.HasOne(d => d.IdКлиентаNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdКлиента)
                .HasConstraintName("Tickets_ibfk_1");
        });

        modelBuilder.Entity<Бронирование>(entity =>
        {
            entity.HasKey(e => e.IdБронирования).HasName("PRIMARY");

            entity.ToTable("Бронирование");

            entity.HasIndex(e => e.IdКлиента, "ID_Клиента");

            entity.HasIndex(e => e.IdМаршрута, "ID_Маршрута");

            entity.Property(e => e.IdБронирования)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Бронирования");
            entity.Property(e => e.IdКлиента)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Клиента");
            entity.Property(e => e.IdМаршрута)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Маршрута");
            entity.Property(e => e.ДатаБронирования)
                .HasColumnType("datetime")
                .HasColumnName("Дата_Бронирования");

            entity.HasOne(d => d.IdКлиентаNavigation).WithMany(p => p.Бронированиеs)
                .HasForeignKey(d => d.IdКлиента)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Бронирование_ibfk_1");

            entity.HasOne(d => d.IdМаршрутаNavigation).WithMany(p => p.Бронированиеs)
                .HasForeignKey(d => d.IdМаршрута)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Бронирование_ibfk_2");
        });

        modelBuilder.Entity<Города>(entity =>
        {
            entity.HasKey(e => e.IdГорода).HasName("PRIMARY");

            entity.ToTable("Города");

            entity.Property(e => e.IdГорода)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Города");
            entity.Property(e => e.НазваниеГорода)
                .HasMaxLength(255)
                .HasColumnName("Название_Города");
        });

        modelBuilder.Entity<Клиент>(entity =>
        {
            entity.HasKey(e => e.IdКлиента).HasName("PRIMARY");

            entity.ToTable("Клиент");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.IdКлиента)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Клиента");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Salt).HasMaxLength(255);
            entity.Property(e => e.Имя).HasMaxLength(255);
            entity.Property(e => e.Фамилия).HasMaxLength(255);
        });

        modelBuilder.Entity<Перевозчик>(entity =>
        {
            entity.HasKey(e => e.IdПеревозчика).HasName("PRIMARY");

            entity.ToTable("Перевозчик");

            entity.Property(e => e.IdПеревозчика)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Перевозчика");
            entity.Property(e => e.НазваниеПеревозчика)
                .HasMaxLength(255)
                .HasColumnName("Название_Перевозчика");
        });

        modelBuilder.Entity<СвободныеМаршруты>(entity =>
        {
            entity.HasKey(e => e.IdМаршрута).HasName("PRIMARY");

            entity.ToTable("Свободные_Маршруты");

            entity.HasIndex(e => e.IdГородаОтправления, "ID_Города_Отправления");

            entity.HasIndex(e => e.IdГородаПрибытия, "ID_Города_Прибытия");

            entity.HasIndex(e => e.IdПеревозчика, "ID_Перевозчика");

            entity.Property(e => e.IdМаршрута)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Маршрута");
            entity.Property(e => e.IdГородаОтправления)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Города_Отправления");
            entity.Property(e => e.IdГородаПрибытия)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Города_Прибытия");
            entity.Property(e => e.IdПеревозчика)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Перевозчика");
            entity.Property(e => e.ДатаОтправления)
                .HasColumnType("datetime")
                .HasColumnName("Дата_Отправления");
            entity.Property(e => e.Цена).HasPrecision(10, 2);

            entity.HasOne(d => d.IdГородаОтправленияNavigation).WithMany(p => p.СвободныеМаршрутыIdГородаОтправленияNavigations)
                .HasForeignKey(d => d.IdГородаОтправления)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Свободные_Маршруты_ibfk_2");

            entity.HasOne(d => d.IdГородаПрибытияNavigation).WithMany(p => p.СвободныеМаршрутыIdГородаПрибытияNavigations)
                .HasForeignKey(d => d.IdГородаПрибытия)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Свободные_Маршруты_ibfk_3");

            entity.HasOne(d => d.IdПеревозчикаNavigation).WithMany(p => p.СвободныеМаршрутыs)
                .HasForeignKey(d => d.IdПеревозчика)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Свободные_Маршруты_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
