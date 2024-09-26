﻿namespace ControllerAPI_1721030861.Database.Models.Bai1;

public partial class Ward
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string WardCode { get; set; } = null!;

    public int DistrictId { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}

public partial class WardDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string WardCode { get; set; } = null!;

    public int DistrictId { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
