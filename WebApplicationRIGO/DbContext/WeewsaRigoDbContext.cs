using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplicationRIGO.Models;

namespace WebApplicationRIGO.DbContext;

public partial class WeewsaRigoDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public WeewsaRigoDbContext()
    {
    }

    public WeewsaRigoDbContext(DbContextOptions<WeewsaRigoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=194.67.105.79;Database=weewsa_rigo_db;Username=weewsa_rigo_user;Password=*****");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("passengers_pk");

            entity.ToTable("passengers");

            entity.HasIndex(e => e.Id, "passengers_id_uindex").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TripId).HasColumnName("trip_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trips_pk");

            entity.ToTable("trips");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArrivalPlace)
                .HasMaxLength(500)
                .HasColumnName("arrival_place");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.DeparturePlace)
                .HasMaxLength(500)
                .HasColumnName("departure_place");
            entity.Property(e => e.DepartureTime).HasColumnName("departure_time");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.MaxPassengers).HasColumnName("max_passengers");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.TripType).HasColumnName("trip_type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactUrl)
                .HasMaxLength(100)
                .HasColumnName("contact_url");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(200)
                .HasColumnName("photo_url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
