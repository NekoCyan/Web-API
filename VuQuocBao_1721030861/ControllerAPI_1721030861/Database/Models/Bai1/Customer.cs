using System;
using System.Collections.Generic;

namespace ControllerAPI_1721030861.Database.Models.Bai1;

public partial class Customer
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Phone { get; set; }

    public int? AddressId { get; set; }

    public int? AccountId { get; set; }

    public int? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

public partial class CustomerDTO
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Phone { get; set; }

    public int? AddressId { get; set; }

    public int? AccountId { get; set; }

    public int? Status { get; set; }
}
