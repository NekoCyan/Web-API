namespace ControllerAPI_1721030861.Database.Models.Bai2;

public partial class School
{
    public int Id { get; set; }

    public string? SchoolCode { get; set; }

    /// <summary>
    /// 1: Tiểu học; 2: TH Cơ sở; 3: THPT; 4: Trung cấp; 5: Cao đẳng; 6: Đại học; 7: Thạc sĩ; 8: Tiến sĩ
    /// </summary>
    public int? SchoolLevel { get; set; }

    public int? CountryId { get; set; }

    public int? ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public string? NameEn { get; set; }

    public int? Status { get; set; }

    public string? Remark { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime Timer { get; set; }
}

public partial class SchoolDTO
{
    public int Id { get; set; }

    public string? SchoolCode { get; set; }

    /// <summary>
    /// 1: Tiểu học; 2: TH Cơ sở; 3: THPT; 4: Trung cấp; 5: Cao đẳng; 6: Đại học; 7: Thạc sĩ; 8: Tiến sĩ
    /// </summary>
    public int? SchoolLevel { get; set; }

    public int? CountryId { get; set; }

    public int? ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public string? NameEn { get; set; }

    public int? Status { get; set; }

    public string? Remark { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime Timer { get; set; }
}
