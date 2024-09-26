namespace ControllerAPI_1721030861.Database.Models.Bai2;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NameSlug { get; set; }

    public string? CountryCode { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public string? Remark { get; set; }

    public DateTime Timer { get; set; }

    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}

public partial class CountryDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NameSlug { get; set; }

    public string? CountryCode { get; set; }

    public int? Status { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string? Remark { get; set; }

    public DateTime Timer { get; set; }
}
