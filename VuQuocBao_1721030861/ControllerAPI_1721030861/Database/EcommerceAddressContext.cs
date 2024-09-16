using ControllerAPI_1721030861.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Database;

public partial class EcommerceAddressContext : DbContext
{
    public EcommerceAddressContext()
    {
    }

    public EcommerceAddressContext(DbContextOptions<EcommerceAddressContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=EcommerceAddress; Persist Security Info=True; User ID=sa; Password=123456; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__CAA247C892DA05D7");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("address_id");
            entity.Property(e => e.AddressText)
                .HasMaxLength(50)
                .HasColumnName("address_text");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Latitude)
                .HasMaxLength(30)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasMaxLength(30)
                .HasColumnName("longitude");
            entity.Property(e => e.Notes)
                .HasMaxLength(100)
                .HasColumnName("notes");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TownId).HasColumnName("town_id");
            entity.Property(e => e.WardId).HasColumnName("ward_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADDRESS_COUNTRY");

            entity.HasOne(d => d.District).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADDRESS_DISTRICT");

            entity.HasOne(d => d.Province).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADDRESS_PROVINCE");

            entity.HasOne(d => d.Ward).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADDRESS_WARD");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__7E8CD05514D451E7");

            entity.ToTable("Country");

            entity.Property(e => e.CountryId)
                .ValueGeneratedNever()
                .HasColumnName("country_id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(50)
                .HasColumnName("country_code");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(50)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(50)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK__District__2521322B0602174F");

            entity.ToTable("District");

            entity.Property(e => e.DistrictId)
                .ValueGeneratedNever()
                .HasColumnName("district_id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(50)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("district_code");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(50)
                .HasColumnName("district_name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(50)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PK__Province__08DCB60F91B10B6E");

            entity.ToTable("Province");

            entity.Property(e => e.ProvinceId)
                .ValueGeneratedNever()
                .HasColumnName("province_id");
            entity.Property(e => e.AxisMeridian)
                .HasMaxLength(50)
                .HasColumnName("axis_meridian");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(50)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(50)
                .HasColumnName("province_code");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(50)
                .HasColumnName("province_name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(50)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.WardId).HasName("PK__Ward__396B899D7296F105");

            entity.ToTable("Ward");

            entity.Property(e => e.WardId)
                .ValueGeneratedNever()
                .HasColumnName("ward_id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(50)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(50)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WardCode)
                .HasMaxLength(50)
                .HasColumnName("ward_code");
            entity.Property(e => e.WardName)
                .HasMaxLength(50)
                .HasColumnName("ward_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public bool IsAddressExists(int? id)
    {
        if (id is null) return false;
        return Addresses.Any(e => e.AddressId == id);
    }

    public bool IsCountryExists(int? id)
    {
        if (id is null) return false;
        return Countries.Any(e => e.CountryId == id);
    }

    public bool IsDistrictExists(int? id)
    {
        if (id is null) return false;
        return Districts.Any(e => e.DistrictId == id);
    }

    public bool IsProvinceExists(int? id)
    {
        if (id is null) return false;
        return Provinces.Any(e => e.ProvinceId == id);
    }

    public bool IsWardExists(int? id)
    {
        if (id is null) return false;
        return Wards.Any(e => e.WardId == id);
    }
}
