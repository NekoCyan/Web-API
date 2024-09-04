using ControllerAPI.Database;
using ControllerAPI.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EFCoreDemoController : ControllerBase
    {
        private readonly EFCoreDemoContext _context;

        public EFCoreDemoController(EFCoreDemoContext context)
        {
            _context = context;
        }

        [HttpGet] // Bai 1
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products
                .ToListAsync();
        }

        [HttpGet] // Bai 2
        public async Task<ActionResult<IEnumerable<string>>> GetAllNamesOfProduct()
        {
            return await _context.Products
                .Select(x => x.ProductName)
                .ToListAsync();
        }

        [HttpGet] // Bai 3
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatNotDiscontinued() // Meaning Continue
        {
            return await _context.Products
                .Where(x => !x.Discontinued)
                .ToListAsync();
        }

        [HttpGet] // Bai 4
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatHavePricesMoreThan20()
        {
            return await _context.Products
                .Where(x => x.UnitPrice > 20)
                .ToListAsync();
        }

        [HttpGet] // Bai 5
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatHaveNameByAscending()
        {
            return await _context.Products
                .OrderBy(x => x.ProductName)
                .ToListAsync();
        }

        [HttpGet] // Bai 6
        public async Task<ActionResult<int>> GetProductsCount()
        {
            return await _context.Products.CountAsync();
        }

        public class ProductNameAndSupplierName
        {
            public string ProductName { get; set; } = null!;
            public string CompanyName { get; set; } = null!;
            public ProductNameAndSupplierName(Product product)
            {
                ProductName = product.ProductName;
                CompanyName = product.Supplier!.CompanyName;
            }
        }
        [HttpGet] // Bai 7
        public async Task<ActionResult<IEnumerable<ProductNameAndSupplierName>>> GetAllProductsNameAndNameOfSupplier()
        {
            return await _context.Products
                .Include(x => x.Supplier)
                .Select(x => new ProductNameAndSupplierName(x))
                .ToListAsync();
        }

        [HttpGet] // Bai 8
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatGroupByCategoryId([Required] int CategoryId)
        {
            return await _context.Products
                .Where(x => x.CategoryId == CategoryId)
                .ToListAsync();
        }

        [HttpGet] // Bai 9
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatGroupBySupplierId([Required] int SupplierId)
        {
            return await _context.Products
                .Where(x => x.SupplierId == SupplierId)
                .ToListAsync();
        }

        [HttpGet] // Bai 10
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliersThatHaveAtLeastOneProduct()
        {
            return await _context.Suppliers
                .Where(x => x.Products.Count > 0)
                .ToListAsync();
        }

        [HttpGet] // Bai 11
        public async Task<ActionResult<Product>> GetProductThatHighestUnitPrice()
        {
            return await _context.Products.OrderByDescending(x => x.UnitPrice).FirstOrDefaultAsync();
        }

        [HttpGet] // Bai 12
        public async Task<ActionResult<Product>> GetProductThatLowestUnitPrice()
        {
            return await _context.Products.OrderBy(x => x.UnitPrice).FirstOrDefaultAsync();
        }

        [HttpGet] // Bai 13
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatIncludeWithCategoryName()
        {
            return await _context.Products
                .Include(x => x.Category)
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    SupplierId = x.SupplierId,
                    CategoryId = x.CategoryId,
                    QuantityPerUnit = x.QuantityPerUnit,
                    UnitPrice = x.UnitPrice,
                    Discontinued = x.Discontinued,
                    Category = x.Category,
                })
                .ToListAsync();
        }
    }
}
