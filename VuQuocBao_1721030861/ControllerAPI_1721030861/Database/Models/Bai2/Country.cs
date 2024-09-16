namespace ControllerAPI_1721030861.Database.Models.Bai2;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public int ProvinceId { get; set; }

    public bool Status { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string UpdatedAt { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}

public partial class CountryDTO
{
    public string CountryName { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public int ProvinceId { get; set; }

    public bool Status { get; set; }
}
