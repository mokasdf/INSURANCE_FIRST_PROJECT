using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace INSURANCE_FIRST_PROJECT.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; }

    public virtual DbSet<Contactu> Contactus { get; set; }

    public virtual DbSet<Home> Homes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subcrebtion> Subcrebtions { get; set; }

    public virtual DbSet<Subcrebtiontype> Subcrebtiontypes { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    public virtual DbSet<Virtualbank> Virtualbanks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##INSURANCE;Password=TEST123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##INSURANCE")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008430");

            entity.ToTable("ABOUT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Image1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE1");
            entity.Property(e => e.Image2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE2");
            entity.Property(e => e.Image3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE3");
            entity.Property(e => e.Image4)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE4");
            entity.Property(e => e.Image5)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE5");
            entity.Property(e => e.Image6)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE6");
            entity.Property(e => e.Text1)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT1");
            entity.Property(e => e.Text2)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT2");
            entity.Property(e => e.Text3)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT3");
            entity.Property(e => e.Text4)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT4");
            entity.Property(e => e.Text5)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT5");
            entity.Property(e => e.Text6)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT6");
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008441");

            entity.ToTable("BENEFICIARY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Beneficiarystartdate)
                .HasColumnType("DATE")
                .HasColumnName("BENEFICIARYSTARTDATE");
            entity.Property(e => e.Document)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("DOCUMENT");
            entity.Property(e => e.Extra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EXTRA");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Relationtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RELATIONTYPE");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.Subcrebtionid)
                .HasColumnType("NUMBER")
                .HasColumnName("SUBCREBTIONID");

            entity.HasOne(d => d.Subcrebtion).WithMany(p => p.Beneficiaries)
                .HasForeignKey(d => d.Subcrebtionid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008442");
        });

        modelBuilder.Entity<Contactu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008432");

            entity.ToTable("CONTACTUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Image1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE1");
            entity.Property(e => e.Image2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE2");
            entity.Property(e => e.Image3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE3");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Text1)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT1");
            entity.Property(e => e.Text2)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT2");
            entity.Property(e => e.Text3)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT3");
            entity.Property(e => e.Text4)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT4");
        });

        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008428");

            entity.ToTable("HOME");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Image1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE1");
            entity.Property(e => e.Image2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE2");
            entity.Property(e => e.Image3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE3");
            entity.Property(e => e.Image4)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE4");
            entity.Property(e => e.Image5)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE5");
            entity.Property(e => e.Image6)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE6");
            entity.Property(e => e.Text1)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT1");
            entity.Property(e => e.Text2)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT2");
            entity.Property(e => e.Text3)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT3");
            entity.Property(e => e.Text4)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT4");
            entity.Property(e => e.Text5)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT5");
            entity.Property(e => e.Text6)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT6");
            entity.Property(e => e.Text7)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008422");

            entity.ToTable("ROLE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Extra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EXTRA");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Subcrebtion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008437");

            entity.ToTable("SUBCREBTION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Extra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EXTRA");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.Subcrebtiondate)
                .HasColumnType("DATE")
                .HasColumnName("SUBCREBTIONDATE");
            entity.Property(e => e.Subcrebtiontypeid)
                .HasColumnType("NUMBER")
                .HasColumnName("SUBCREBTIONTYPEID");
            entity.Property(e => e.Useraccountid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERACCOUNTID");

            entity.HasOne(d => d.Subcrebtiontype).WithMany(p => p.Subcrebtions)
                .HasForeignKey(d => d.Subcrebtiontypeid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008438");

            entity.HasOne(d => d.Useraccount).WithMany(p => p.Subcrebtions)
                .HasForeignKey(d => d.Useraccountid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008439");
        });

        modelBuilder.Entity<Subcrebtiontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008424");

            entity.ToTable("SUBCREBTIONTYPE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            // edited for subs image text1 text2
            entity.Property(e => e.Text1)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT1");
            
            entity.Property(e => e.Image)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE");

            entity.Property(e => e.Text2)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("TEXT2");
            //

            entity.Property(e => e.Maxnumberofbeneficiaries)
                .HasColumnType("NUMBER")
                .HasColumnName("MAXNUMBEROFBENEFICIARIES");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER")
                .HasColumnName("PRICE");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008444");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Extra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EXTRA");
            entity.Property(e => e.Message)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.Testimonialdate)
                .HasColumnType("DATE")
                .HasColumnName("TESTIMONIALDATE");
            entity.Property(e => e.Useraccountid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERACCOUNTID");

            entity.HasOne(d => d.Useraccount).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.Useraccountid)
                .HasConstraintName("SYS_C008445");
        });

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008434");

            entity.ToTable("USERACCOUNT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Extra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EXTRA");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Image)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Password)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");

            entity.HasOne(d => d.Role).WithMany(p => p.Useraccounts)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008435");
        });

        modelBuilder.Entity<Virtualbank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008426");

            entity.ToTable("VIRTUALBANK");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cardnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("CARDNUMBER");
            entity.Property(e => e.Cvv)
                .HasColumnType("NUMBER")
                .HasColumnName("CVV");
            entity.Property(e => e.Expirydate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRYDATE");
            entity.Property(e => e.Extra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EXTRA");
            entity.Property(e => e.Ownername)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OWNERNAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
