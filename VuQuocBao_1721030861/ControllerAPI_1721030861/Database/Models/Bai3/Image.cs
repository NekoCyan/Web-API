using System;
using System.Collections.Generic;

namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class Image
{
    public int Id { get; set; }

    public string? RefId { get; set; }

    public string? SmallUrl { get; set; }

    public string? RelativeUrl { get; set; }

    public string? MediumUrl { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
