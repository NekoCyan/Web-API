using System;
using System.Collections.Generic;
using ControllerAPI_1721030861.Database.Models.Bai3;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Database;

public partial class CmsContext : DbContext
{
    public CmsContext()
    {
    }

    public CmsContext(DbContextOptions<CmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<ContentType> ContentTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Portal> Portals { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=CMS; Persist Security Info=True; User ID=sa; Password=123456; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Account");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "IX_Account_Email").IsUnique();

            entity.HasIndex(e => e.UserName, "IX_Account_Username").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccountType)
                .HasMaxLength(10)
                .HasDefaultValue("USR")
                .HasColumnName("account_type");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Birthday)
                .HasColumnType("datetime")
                .HasColumnName("birthday");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("full_name");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("notes");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("user_name");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Account_AccountType");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_AccountType");

            entity.ToTable("AccountType");

            entity.HasIndex(e => e.Code, "IX_AccountType").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("notes");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Addresses");

            entity.ToTable("Address");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AddressText)
                .HasMaxLength(100)
                .HasColumnName("address_text");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TownId).HasColumnName("town_id");
            entity.Property(e => e.WardId).HasColumnName("ward_id");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.ToTable("Banner");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClickNumber).HasColumnName("click_number");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DescShort)
                .HasMaxLength(250)
                .HasColumnName("desc_short");
            entity.Property(e => e.EndAt)
                .HasColumnType("datetime")
                .HasColumnName("end_at");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("notes");
            entity.Property(e => e.RefId)
                .HasMaxLength(36)
                .HasColumnName("ref_id");
            entity.Property(e => e.StartAt)
                .HasColumnType("datetime")
                .HasColumnName("start_at");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_comment");

            entity.ToTable("Comment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("full_name");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contact");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasComment("The member you want to contact has an account")
                .HasColumnName("account_id");
            entity.Property(e => e.AdviseId)
                .HasComment("Account is adviser")
                .HasColumnName("advise_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValue(0)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("full_name");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Respponse).HasColumnName("respponse");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Detail).HasColumnName("detail");
            entity.Property(e => e.HomePage).HasColumnName("home_page");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImageList)
                .HasMaxLength(100)
                .HasColumnName("image_list");
            entity.Property(e => e.IsHot).HasColumnName("is_hot");
            entity.Property(e => e.LikeNumber).HasColumnName("like_number");
            entity.Property(e => e.LinkRef)
                .HasMaxLength(200)
                .HasColumnName("link_ref");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("datetime")
                .HasColumnName("published_at");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.TitleSlug)
                .HasMaxLength(200)
                .HasColumnName("title_slug");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.ViewNumber).HasColumnName("view_number");

            entity.HasOne(d => d.ContentType).WithMany(p => p.Contents)
                .HasForeignKey(d => d.ContentTypeId)
                .HasConstraintName("FK_Contents_ContentType");
        });

        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.ToTable("ContentType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("notes");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.PortalId).HasColumnName("portal_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(150)
                .HasColumnName("remark");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.TitleSlug)
                .HasMaxLength(100)
                .HasColumnName("title_slug");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(5)
                .HasColumnName("country_code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_District");

            entity.ToTable("District");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(5)
                .HasColumnName("district_code");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District_Province");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3213E83F47534C37");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.MediumUrl)
                .HasMaxLength(250)
                .HasColumnName("medium_url");
            entity.Property(e => e.RefId)
                .HasMaxLength(36)
                .HasColumnName("ref_id");
            entity.Property(e => e.RelativeUrl)
                .HasMaxLength(250)
                .HasColumnName("relative_url");
            entity.Property(e => e.SmallUrl)
                .HasMaxLength(250)
                .HasColumnName("small_url");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Portal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Customer");

            entity.ToTable("Portal");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.BannerUrl)
                .HasMaxLength(200)
                .HasColumnName("banner_url");
            entity.Property(e => e.ContactName)
                .HasMaxLength(150)
                .HasColumnName("contact_name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Facebook)
                .HasMaxLength(200)
                .HasColumnName("facebook");
            entity.Property(e => e.Hotline)
                .HasMaxLength(20)
                .HasColumnName("hotline");
            entity.Property(e => e.ImageFavicon)
                .HasMaxLength(200)
                .HasColumnName("image_favicon");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImageList)
                .HasMaxLength(200)
                .HasColumnName("image_list");
            entity.Property(e => e.Instagram)
                .HasMaxLength(200)
                .HasColumnName("instagram");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.NameSlug)
                .HasMaxLength(300)
                .HasColumnName("name_slug");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PostCode)
                .HasMaxLength(10)
                .HasColumnName("post_code");
            entity.Property(e => e.RefCode)
                .HasMaxLength(36)
                .HasColumnName("ref_code");
            entity.Property(e => e.SiteUrl)
                .HasMaxLength(200)
                .HasColumnName("site_url");
            entity.Property(e => e.SocialNetwork).HasColumnName("social_network");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TaxNumber)
                .HasMaxLength(20)
                .HasColumnName("tax_number");
            entity.Property(e => e.Twitter)
                .HasMaxLength(200)
                .HasColumnName("twitter");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Youtube)
                .HasMaxLength(200)
                .HasColumnName("youtube");
            entity.Property(e => e.Zalo)
                .HasMaxLength(200)
                .HasColumnName("zalo");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Province");

            entity.ToTable("Province");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AxisMeridian)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("axis_meridian");
            entity.Property(e => e.CountryId)
                .HasDefaultValue(1)
                .HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(5)
                .HasColumnName("province_code");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ExpiredAt).HasColumnName("expiredAt");
            entity.Property(e => e.IsRevoked).HasColumnName("isRevoked");
            entity.Property(e => e.IsUsed).HasColumnName("isUsed");
            entity.Property(e => e.IssuedAt).HasColumnName("issuedAt");
            entity.Property(e => e.JwtId)
                .HasMaxLength(50)
                .HasColumnName("jwtId");
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .HasColumnName("notes");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Token)
                .HasMaxLength(50)
                .HasColumnName("token");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Ward");

            entity.ToTable("Ward");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WardCode)
                .HasMaxLength(5)
                .HasColumnName("ward_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
