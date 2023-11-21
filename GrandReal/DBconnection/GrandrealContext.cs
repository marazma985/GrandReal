using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrandReal.DBconnection;

public partial class GrandrealContext : DbContext
{
    public GrandrealContext()
    {
    }

    public GrandrealContext(DbContextOptions<GrandrealContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<FavoriteClientObject> FavoriteClientObjects { get; set; }

    public virtual DbSet<ImagesObject> ImagesObjects { get; set; }

    public virtual DbSet<Object> Objects { get; set; }

    public virtual DbSet<ObjectView> ObjectViews { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<TypeObject> TypeObjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=grandreal;user=root;password=Marmel985.", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("applications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCreate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("date_create");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdObject).HasColumnName("id_object");
            entity.Property(e => e.IdStaff).HasColumnName("id_staff");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.IdClient)
                .ValueGeneratedNever()
                .HasColumnName("id_client");
            entity.Property(e => e.EmailClient)
                .HasMaxLength(50)
                .HasColumnName("email_client");
            entity.Property(e => e.NameClient)
                .HasMaxLength(50)
                .HasColumnName("name_client");
            entity.Property(e => e.PasswordClient)
                .HasMaxLength(50)
                .HasColumnName("password_client");
            entity.Property(e => e.PatronymicClient)
                .HasMaxLength(50)
                .HasColumnName("patronymic_client");
            entity.Property(e => e.PhoneClient)
                .HasMaxLength(40)
                .HasColumnName("phone_client");
            entity.Property(e => e.RequisitesClient)
                .HasMaxLength(50)
                .HasColumnName("requisites_client");
            entity.Property(e => e.SurnameClient)
                .HasMaxLength(50)
                .HasColumnName("surname_client");
            entity.Property(e => e.ViewClient).HasColumnName("view_client");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.IdContract).HasName("PRIMARY");

            entity.ToTable("contract");

            entity.HasIndex(e => e.IdByerContract, "clien_idx");

            entity.HasIndex(e => e.IdObjectContract, "ob_idx");

            entity.HasIndex(e => e.IdStaff, "sta_idx");

            entity.Property(e => e.IdContract)
                .ValueGeneratedNever()
                .HasColumnName("id_contract");
            entity.Property(e => e.DateConclusionContract).HasColumnName("dateConclusion_contract");
            entity.Property(e => e.IdByerContract).HasColumnName("idByer_contract");
            entity.Property(e => e.IdObjectContract).HasColumnName("idObject_contract");
            entity.Property(e => e.IdStaff).HasColumnName("idStaff");
            entity.Property(e => e.ValidUntilContract).HasColumnName("validUntil_contract");
            entity.Property(e => e.ViewContract).HasColumnName("view_contract");

            entity.HasOne(d => d.IdByerContractNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.IdByerContract)
                .HasConstraintName("clien");

            entity.HasOne(d => d.IdObjectContractNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.IdObjectContract)
                .HasConstraintName("ob");

            entity.HasOne(d => d.IdStaffNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.IdStaff)
                .HasConstraintName("sta");
        });

        modelBuilder.Entity<FavoriteClientObject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("favorite_client_object");

            entity.HasIndex(e => e.Client, "cli_idx");

            entity.HasIndex(e => e.Object, "obje_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.Object).HasColumnName("object");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.FavoriteClientObjects)
                .HasForeignKey(d => d.Client)
                .HasConstraintName("cli");

            entity.HasOne(d => d.ObjectNavigation).WithMany(p => p.FavoriteClientObjects)
                .HasForeignKey(d => d.Object)
                .HasConstraintName("obje");
        });

        modelBuilder.Entity<ImagesObject>(entity =>
        {
            entity.HasKey(e => e.IdImage).HasName("PRIMARY");

            entity.ToTable("images_object");

            entity.HasIndex(e => e.Object, "objectIm_idx");

            entity.Property(e => e.IdImage)
                .ValueGeneratedNever()
                .HasColumnName("id_image");
            entity.Property(e => e.NameImage)
                .HasMaxLength(45)
                .HasColumnName("name_image");
            entity.Property(e => e.Object).HasColumnName("object");

            entity.HasOne(d => d.ObjectNavigation).WithMany(p => p.ImagesObjects)
                .HasForeignKey(d => d.Object)
                .HasConstraintName("objectIm");
        });

        modelBuilder.Entity<Object>(entity =>
        {
            entity.HasKey(e => e.IdObject).HasName("PRIMARY");

            entity.ToTable("object");

            entity.HasIndex(e => e.IdSobObject, "sob_idx");

            entity.HasIndex(e => e.TypeObject, "type_idx");

            entity.Property(e => e.IdObject)
                .ValueGeneratedNever()
                .HasColumnName("id_object");
            entity.Property(e => e.AddressObject)
                .HasMaxLength(100)
                .HasColumnName("address_object");
            entity.Property(e => e.CityObject)
                .HasMaxLength(100)
                .HasColumnName("city_object");
            entity.Property(e => e.DistrictObject)
                .HasMaxLength(100)
                .HasColumnName("district_object");
            entity.Property(e => e.Flat).HasColumnName("flat");
            entity.Property(e => e.FloorObject).HasColumnName("floor_object");
            entity.Property(e => e.IdSobObject).HasColumnName("idSob_object");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_active");
            entity.Property(e => e.LivingAreaObject).HasColumnName("livingArea_object");
            entity.Property(e => e.NumRoomsObject).HasColumnName("numRooms_object");
            entity.Property(e => e.PlotAreaObject).HasColumnName("plotArea_object");
            entity.Property(e => e.PriceObject)
                .HasPrecision(19, 2)
                .HasColumnName("price_object");
            entity.Property(e => e.TotalFloor).HasColumnName("total_floor");
            entity.Property(e => e.TypeObject).HasColumnName("type_object");

            entity.HasOne(d => d.IdSobObjectNavigation).WithMany(p => p.Objects)
                .HasForeignKey(d => d.IdSobObject)
                .HasConstraintName("sob");

            entity.HasOne(d => d.TypeObjectNavigation).WithMany(p => p.Objects)
                .HasForeignKey(d => d.TypeObject)
                .HasConstraintName("type");
        });

        modelBuilder.Entity<ObjectView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("object_view");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Cost)
                .HasMaxLength(62)
                .HasColumnName("cost");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.Flat).HasColumnName("flat");
            entity.Property(e => e.Floors)
                .HasMaxLength(27)
                .HasColumnName("floors");
            entity.Property(e => e.FullAddress)
                .HasMaxLength(315)
                .HasColumnName("full_address");
            entity.Property(e => e.IdObject).HasColumnName("id_object");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_active");
            entity.Property(e => e.LivingArea).HasColumnName("living_area");
            entity.Property(e => e.PlotArea).HasColumnName("plot_area");
            entity.Property(e => e.Rooms).HasColumnName("rooms");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.IdStaff).HasName("PRIMARY");

            entity.ToTable("staff");

            entity.Property(e => e.IdStaff)
                .ValueGeneratedNever()
                .HasColumnName("id_staff");
            entity.Property(e => e.NameStaff)
                .HasMaxLength(50)
                .HasColumnName("name_staff");
            entity.Property(e => e.PasswordStaff)
                .HasMaxLength(45)
                .HasColumnName("password_staff");
            entity.Property(e => e.PatronymicStaff)
                .HasMaxLength(50)
                .HasColumnName("patronymic_staff");
            entity.Property(e => e.PhoneStaff)
                .HasPrecision(19, 5)
                .HasColumnName("phone_staff");
            entity.Property(e => e.SurnameStaff)
                .HasMaxLength(50)
                .HasColumnName("surname_staff");
            entity.Property(e => e.ViewStaff).HasColumnName("view_staff");
        });

        modelBuilder.Entity<TypeObject>(entity =>
        {
            entity.HasKey(e => e.IdTypeObject).HasName("PRIMARY");

            entity.ToTable("type_object");

            entity.Property(e => e.IdTypeObject)
                .ValueGeneratedNever()
                .HasColumnName("id_typeObject");
            entity.Property(e => e.NameTypeObject)
                .HasMaxLength(50)
                .HasColumnName("name_typeObject");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
