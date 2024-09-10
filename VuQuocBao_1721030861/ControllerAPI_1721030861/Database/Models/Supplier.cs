namespace ControllerAPI_1721030861.Database.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string? Phone { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class SupplierDTO
    {
        public string? CompanyName { get; set; }

        public string? Phone { get; set; }
    }
}
