namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class Banner
{
    public int Id { get; set; }

    public string? RefId { get; set; }

    public string? Title { get; set; }

    public string? DescShort { get; set; }

    public string? Url { get; set; }

    public int? ImageId { get; set; }

    public int? ClickNumber { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public string? Notes { get; set; }
}

public partial class BannerDTO
{
    public int Id { get; set; }

    public string? RefId { get; set; }

    public string? Title { get; set; }

    public string? DescShort { get; set; }

    public string? Url { get; set; }

    public int? ImageId { get; set; }

    public int? ClickNumber { get; set; }

    public string? Notes { get; set; }
}
