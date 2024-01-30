using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightProjApi.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<SanskritiBooking> SanskritiBookings { get; set; }

    public virtual DbSet<SanskritiFlight> SanskritiFlights { get; set; }

    public virtual DbSet<SanskritiLoginDetail> SanskritiLoginDetails { get; set; }

    public virtual DbSet<SanskritiPassenger> SanskritiPassengers { get; set; }

    public virtual DbSet<SanskritiPayment> SanskritiPayments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DEVSQL.Corp.local;Database=ACE 5- 2024;Trusted_Connection=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SanskritiBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__sanskrit__C6D03BCD93C1D1A3");

            entity.ToTable("sanskriti_Bookings");

            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.BookingDate).HasColumnName("bookingDate");
            entity.Property(e => e.CustId).HasColumnName("custId");
            entity.Property(e => e.FlightId).HasColumnName("flightId");
            entity.Property(e => e.NoOfPass).HasColumnName("noOfPass");
            entity.Property(e => e.TotalCost)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("totalCost");

            entity.HasOne(d => d.Cust).WithMany(p => p.SanskritiBookings)
                .HasForeignKey(d => d.CustId)
                .HasConstraintName("FK__sanskriti__custI__07420643");

            entity.HasOne(d => d.Flight).WithMany(p => p.SanskritiBookings)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__sanskriti__fligh__064DE20A");
        });

        modelBuilder.Entity<SanskritiFlight>(entity =>
        {
            entity.HasKey(e => e.Flightid).HasName("PK__sanskrit__0E06BA3AB4FEE00C");

            entity.ToTable("sanskriti_Flight");

            entity.Property(e => e.Flightid).HasColumnName("flightid");
            entity.Property(e => e.ArrAirportId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("arrAirportId");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("arrivalTime");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("datetime")
                .HasColumnName("departureTime");
            entity.Property(e => e.DeptAirportId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("deptAirportId");
            entity.Property(e => e.FarrTerminal).HasColumnName("farrTerminal");
            entity.Property(e => e.FdeptTerminal).HasColumnName("fdeptTerminal");
            entity.Property(e => e.Fdestination)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fdestination");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.Fsource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fsource");
            entity.Property(e => e.NoOfSeats).HasColumnName("noOfSeats");
            entity.Property(e => e.Rate)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("rate");
        });

        modelBuilder.Entity<SanskritiLoginDetail>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__sanskrit__DD37D91A1DCD33B1");

            entity.ToTable("sanskriti_LoginDetails");

            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lname");
            entity.Property(e => e.Pword)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pword");
        });

        modelBuilder.Entity<SanskritiPassenger>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__sanskrit__DD37D91ABDF9929C");

            entity.ToTable("sanskriti_Passenger");

            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Food)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("food");
            entity.Property(e => e.Paddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("paddress");
            entity.Property(e => e.Pemail)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("pemail");
            entity.Property(e => e.Pfname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pfname");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phoneNo");
            entity.Property(e => e.Plname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("plname");
            entity.Property(e => e.SeatNo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("seatNo");
        });

        modelBuilder.Entity<SanskritiPayment>(entity =>
        {
            entity.HasKey(e => e.Accno).HasName("PK__sanskrit__A472931D4CF90278");

            entity.ToTable("sanskriti_Payments");

            entity.Property(e => e.Accno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("accno");
            entity.Property(e => e.CardHolderName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("cardHolderName");
            entity.Property(e => e.Cvv).HasColumnName("cvv");
            entity.Property(e => e.Payid).HasColumnName("payid");
            entity.Property(e => e.ValidUpto).HasColumnName("validUpto");

            entity.HasOne(d => d.Pay).WithMany(p => p.SanskritiPayments)
                .HasForeignKey(d => d.Payid)
                .HasConstraintName("FK__sanskriti__payid__027D5126");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
