using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HotelPairs.Services
{
    public partial class HotelDbContext : DbContext
    {
        public HotelDbContext()
            : base("name=HotelDbContext")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingService> BookingServices { get; set; }
        public virtual DbSet<CleaningSchedule> CleaningSchedules { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoomCard> RoomCards { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasMany(e => e.BookingServices)
                .WithRequired(e => e.Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.Services)
                .WithRequired(e => e.Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Guest>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Guest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.TotalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.CleaningSchedules)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.RoomCards)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.BookingServices)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.CleaningSchedules)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Type)
                .WillCascadeOnDelete(false);
        }
    }
}
