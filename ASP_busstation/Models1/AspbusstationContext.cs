using System;
using System.Collections.Generic;
using ASP_busstation.Models1;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_busstation.Models1;

public partial class AspbusstationContext : IdentityDbContext<User>
{
    //protected readonly IConfiguration _configuration;
    //public AspbusstationContext(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    public AspbusstationContext()
    {

    }

    public AspbusstationContext(DbContextOptions<AspbusstationContext> options) : base(options)
    {

    }

    #region entities
    public virtual DbSet<BusRoute> BusRoutes { get; set; }
    public virtual DbSet<BusShelter> BusShelters { get; set; }
    public virtual DbSet<Driver> Drivers { get; set; }
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<Settlement> Settlements { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<Transport> Transports { get; set; }
    public virtual DbSet<TransportBrand> TransportBrands { get; set; }
    public virtual DbSet<TransportCategory> TransportCategories { get; set; }
    //public virtual DbSet<User> Users { get; set; }
    //public virtual DbSet<UserType> UserTypes { get; set; }
    public virtual DbSet<Voyage> Voyages { get; set; }
    public virtual DbSet<VoyageStatus> VoyageStatuses { get; set; }
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder option)
    {
        option.UseSqlServer("Server=laptop-4vtejnu0\\SQLEXPRESS;Database=ASPbusstation2;Trusted_Connection=True;Encrypt=False;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BusRoute>(entity =>
        {
            entity.ToTable("BusRoute");
        });

        modelBuilder.Entity<BusShelter>(entity =>
        {
            entity.ToTable("BusShelter");

            entity.Property(e => e.BusRouteFk).HasColumnName("BusRouteFK");
            entity.Property(e => e.SettlementFk).HasColumnName("SettlementFK");

            entity.HasOne(d => d.BusRouteFkNavigation).WithMany(p => p.BusShelters)
                .HasForeignKey(d => d.BusRouteFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusShelter_BusRoute");

            entity.HasOne(d => d.SettlementFkNavigation).WithMany(p => p.BusShelters)
                .HasForeignKey(d => d.SettlementFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusShelter_Settlement");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.ToTable("Driver");

            entity.Property(e => e.DrivingLicense)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIO");
            entity.Property(e => e.Passport)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("Region");

            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Settlement>(entity =>
        {
            entity.ToTable("Settlement");

            entity.Property(e => e.RegionFk).HasColumnName("RegionFK");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.RegionFkNavigation).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.RegionFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settlement_Region");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.Property(e => e.PassengerPassport)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserFk).HasColumnName("UserFK");
            entity.Property(e => e.VoyageFk).HasColumnName("VoyageFK");

            //entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.Tickets)
            //    .HasForeignKey(d => d.UserFk)
            //    .HasConstraintName("FK_Ticket_User");

            entity.HasOne(d => d.VoyageFkNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.VoyageFk)
                .HasConstraintName("FK_Ticket_Voyage");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.ToTable("Transport");

            entity.Property(e => e.CarNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransportBrandFk).HasColumnName("TransportBrandFK");
            entity.Property(e => e.TransportCategoryFk).HasColumnName("TransportCategoryFK");

            entity.HasOne(d => d.TransportBrandFkNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.TransportBrandFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transport_TransportBrand");

            entity.HasOne(d => d.TransportCategoryFkNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.TransportCategoryFk)
                .HasConstraintName("FK_Transport_TransportCategory");
        });

        modelBuilder.Entity<TransportBrand>(entity =>
        {
            entity.ToTable("TransportBrand");

            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransportCategory>(entity =>
        {
            entity.ToTable("TransportCategory");

            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        //modelBuilder.Entity<User>(entity =>
        //{
        //    entity.ToTable("User");

        //    entity.Property(e => e.Fio)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasColumnName("FIO");
        //    entity.Property(e => e.Login)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.Password)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.UserTypeFk).HasColumnName("UserTypeFK");

        //    entity.HasOne(d => d.UserTypeFkNavigation).WithMany(p => p.Users)
        //        .HasForeignKey(d => d.UserTypeFk)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK_User_UserType");
        //});

        //modelBuilder.Entity<UserType>(entity =>
        //{
        //    entity.ToTable("UserType");

        //    entity.Property(e => e.Title)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //});

        modelBuilder.Entity<Voyage>(entity =>
        {
            entity.ToTable("Voyage");

            entity.Property(e => e.AllowTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.DriverFk).HasColumnName("DriverFK");
            entity.Property(e => e.DriverPassport)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RouteFk).HasColumnName("RouteFK");
            entity.Property(e => e.TransportFk).HasColumnName("TransportFK");
            entity.Property(e => e.VoyageStatusFk).HasColumnName("VoyageStatusFK");

            entity.HasOne(d => d.DriverFkNavigation).WithMany(p => p.Voyages)
                .HasForeignKey(d => d.DriverFk)
                .HasConstraintName("FK_Voyage_Driver");

            entity.HasOne(d => d.RouteFkNavigation).WithMany(p => p.Voyages)
                .HasForeignKey(d => d.RouteFk)
                .HasConstraintName("FK_Voyage_BusRoute");

            entity.HasOne(d => d.TransportFkNavigation).WithMany(p => p.Voyages)
                .HasForeignKey(d => d.TransportFk)
                .HasConstraintName("FK_Voyage_Transport");

            entity.HasOne(d => d.VoyageStatusFkNavigation).WithMany(p => p.Voyages)
                .HasForeignKey(d => d.VoyageStatusFk)
                .HasConstraintName("FK_Voyage_VoyageStatus");
        });

        modelBuilder.Entity<VoyageStatus>(entity =>
        {
            entity.ToTable("VoyageStatus");

            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
