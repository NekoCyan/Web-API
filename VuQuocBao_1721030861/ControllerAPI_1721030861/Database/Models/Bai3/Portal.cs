using System;
using System.Collections.Generic;

namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class Portal
{
    public int Id { get; set; }

    public string? RefCode { get; set; }

    public string Name { get; set; } = null!;

    public string? NameSlug { get; set; }

    public string? Description { get; set; }

    public string? ContactName { get; set; }

    public string? TaxNumber { get; set; }

    public int? AddressId { get; set; }

    public int? ImageId { get; set; }

    public string? ImageFavicon { get; set; }

    public string? BannerUrl { get; set; }

    public string? ImageList { get; set; }

    public string? Hotline { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? PostCode { get; set; }

    public string? SocialNetwork { get; set; }

    public string? Facebook { get; set; }

    public string? Twitter { get; set; }

    public string? Instagram { get; set; }

    public string? Zalo { get; set; }

    public string? Youtube { get; set; }

    public string? SiteUrl { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
