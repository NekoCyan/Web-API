namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? CountryCode { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}

public partial class CountryDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? CountryCode { get; set; }
}
