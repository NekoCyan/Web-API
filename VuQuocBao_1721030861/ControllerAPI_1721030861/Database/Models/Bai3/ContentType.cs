namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class ContentType
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Title { get; set; } = null!;

    public string? TitleSlug { get; set; }

    public int? Sort { get; set; }

    public int? ImageId { get; set; }

    public string? Remark { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public int? PortalId { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
}
