namespace ControllerAPI_1721030861.Database.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category? Category { get; set; }

        public virtual Supplier? Supplier { get; set; }
    }

    public class ProductDTO
    {
        public string ProductName { get; set; } = null!;

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool Discontinued { get; set; }
    }
}
