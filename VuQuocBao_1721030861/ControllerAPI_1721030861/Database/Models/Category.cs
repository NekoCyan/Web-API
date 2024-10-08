﻿namespace ControllerAPI_1721030861.Database.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class CategoryDTO
    {
        public string? CategoryName { get; set; }

        public string? Description { get; set; }
    }
}
