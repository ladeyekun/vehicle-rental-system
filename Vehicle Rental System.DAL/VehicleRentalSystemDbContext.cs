using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class VehicleRentalSystemDbContext : IdentityDbContext<IdentityUser> {
        public VehicleRentalSystemDbContext(DbContextOptions<VehicleRentalSystemDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.VehicleId);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<History>()
                .HasKey(h => h.RentalHistoryId);

            modelBuilder.Entity<Location>()
                .HasKey(l => l.LocationId);

            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.ReservationId);

            modelBuilder.Entity<Reservation>()
                .HasOne(c => c.Customer)
                .WithMany(r => r.Reservations)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(v => v.Vehicle)
                .WithMany(r => r.Reservations)
                .HasForeignKey(v => v.VehicleId);

            modelBuilder.Entity<History>()
                .HasOne(c => c.Customer)
                .WithMany(h => h.Histories)
                .HasForeignKey(h => h.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<History>()
                .HasOne(v => v.Vehicle)
                .WithMany(h => h.Histories)
                .HasForeignKey(h => h.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reservation)
                .WithOne(rs => rs.Review)
                .HasForeignKey<Review>(r => r.ReservationId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Payment)
                .WithOne(p => p.Reservation)
                .HasForeignKey<Payment>(p => p.ReservationId);

            modelBuilder.Entity<Customer>(entity => {
                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(15);
                entity.Property(e => e.CustomerGender).IsRequired().HasMaxLength(6);
                entity.Property(e => e.DateOfBirth).IsRequired().HasMaxLength(20);
                entity.Property(e => e.DriversLicenseId).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<History>(entity => {
                entity.Property(e => e.StartDate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.EndDate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.TotalCost).IsRequired();
            });

            modelBuilder.Entity<Location>(entity => {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Payment>(entity => {
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.PaymentDate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Reservation>(entity => {
                entity.Property(e => e.StartDate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.EndDate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Review>(entity => {
                entity.Property(e => e.Message).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ReviewDate).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Vehicle>(entity => {
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(20);
                entity.Property(e => e.RentalPrice).IsRequired();
                entity.Property(e => e.Mileage).IsRequired();
                entity.Property(e => e.VehicleAvailabilityStatus).IsRequired().HasMaxLength(20);

            });
        }
    }
}
