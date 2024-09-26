using System;
using System.Collections.Generic;

namespace ControllerAPI_1721030861.Database.Models.Bai1;

public partial class Shipper
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public int? AddressId { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

public partial class ShipperDTO
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public int? AddressId { get; set; }

    public int? Status { get; set; }
}
