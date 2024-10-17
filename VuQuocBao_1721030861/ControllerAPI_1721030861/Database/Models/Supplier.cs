namespace ControllerAPI_1721030861.Database.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? HomePage { get; set; }

    public int? AddressId { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

public partial class SupplierDTO
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? HomePage { get; set; }

    public int? AddressId { get; set; }

    public int? Status { get; set; }
}
