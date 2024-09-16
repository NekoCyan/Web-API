using System;
using System.Collections.Generic;

namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class Content
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? TitleSlug { get; set; }

    public int? ContentTypeId { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public int? ImageId { get; set; }

    public string? ImageList { get; set; }

    public string? LinkRef { get; set; }

    public int? LikeNumber { get; set; }

    public int? ViewNumber { get; set; }

    public bool? IsHot { get; set; }

    public bool? HomePage { get; set; }

    public int? Sort { get; set; }

    public int? Status { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ContentType? ContentType { get; set; }
}
