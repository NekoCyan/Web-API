﻿using ControllerAPI.Database;
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
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsBai1()
        {
            return await _context.Products
                .ToListAsync();
        }

        [HttpGet] // Bai 2
        public async Task<ActionResult<IEnumerable<string>>> GetAllNamesOfProductBai2()
        {
            return await _context.Products
                .Select(x => x.ProductName)
                .ToListAsync();
        }

        [HttpGet] // Bai 3
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatNotDiscontinuedBai3() // Meaning Continue
        {
            return await _context.Products
                .Where(x => !x.Discontinued)
                .ToListAsync();
        }

        [HttpGet] // Bai 4
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatHavePricesMoreThan20Bai4()
        {
            return await _context.Products
                .Where(x => x.UnitPrice > 20)
                .ToListAsync();
        }

        [HttpGet] // Bai 5
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatHaveNameByAscendingBai5()
        {
            return await _context.Products
                .OrderBy(x => x.ProductName)
                .ToListAsync();
        }

        [HttpGet] // Bai 6
        public async Task<ActionResult<int>> GetProductsCountBai6()
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
        public async Task<ActionResult<IEnumerable<ProductNameAndSupplierName>>> GetAllProductsNameAndNameOfSupplierBai7()
        {
            return await _context.Products
                .Include(x => x.Supplier)
                .Select(x => new ProductNameAndSupplierName(x))
                .ToListAsync();
        }

        [HttpGet] // Bai 8
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatGroupByCategoryIdBai8([Required] int CategoryId)
        {
            return await _context.Products
                .Where(x => x.CategoryId == CategoryId)
                .ToListAsync();
        }

        [HttpGet] // Bai 9
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatGroupBySupplierIdBai9([Required] int SupplierId)
        {
            return await _context.Products
                .Where(x => x.SupplierId == SupplierId)
                .ToListAsync();
        }

        [HttpGet] // Bai 10
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliersThatHaveAtLeastOneProductBai10()
        {
            return await _context.Suppliers
                .Where(x => x.Products.Count > 0)
                .ToListAsync();
        }

        [HttpGet] // Bai 11
        public async Task<ActionResult<Product>> GetProductThatHighestUnitPriceBai11()
        {
            return await _context.Products.OrderByDescending(x => x.UnitPrice).FirstOrDefaultAsync();
        }

        [HttpGet] // Bai 12
        public async Task<ActionResult<Product>> GetProductThatLowestUnitPriceBai12()
        {
            return await _context.Products.OrderBy(x => x.UnitPrice).FirstOrDefaultAsync();
        }

        [HttpGet] // Bai 13
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatIncludeWithCategoryNameBai13()
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

        [HttpGet] // Bai 14
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatIncludeWithSupplierNameBai14()
        {
            return await _context.Products
                .Include(x => x.Supplier)
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    SupplierId = x.SupplierId,
                    CategoryId = x.CategoryId,
                    QuantityPerUnit = x.QuantityPerUnit,
                    UnitPrice = x.UnitPrice,
                    Discontinued = x.Discontinued,
                    Supplier = x.Supplier,
                })
                .ToListAsync();
        }

        [HttpGet] // Bai 15
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatEqualsToAverageUnitPricesOfAllProductsBai15()
        {
            return await _context.Products
                .Where(x => x.UnitPrice == _context.Products.Average(x => x.UnitPrice))
                .ToListAsync();
        }

        [HttpGet] // Bai 16 - bug
        public async Task<ActionResult<IEnumerable<int>>> GetTotalCountOfProductsForEachCategoryBai16()
        {
            return await _context.Categories
                .Select(x => x.Products.Count)
                .ToListAsync();
        }

        public class GetAllCategoriesWithTotalCountOfProducts
        {
            public Category Categories { get; set; }
            public int ProductsCount { get; set; }
            public GetAllCategoriesWithTotalCountOfProducts(Category category)
            {
                ProductsCount = category.Products.Count;
                category.Products = new List<Product>();
                Categories = category;
            }
        }
        [HttpGet] // Bai 17 - bug
        public async Task<ActionResult<IEnumerable<GetAllCategoriesWithTotalCountOfProducts>>> GetAllCategoriesIncludeWithTotalCountOfProductsBai17()
        {
            return await _context.Categories
                .Include(x => x.Products)
                .Select(x => new GetAllCategoriesWithTotalCountOfProducts(x))
                .ToListAsync();
        }

        [HttpGet] // Bai 18
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliersThatHaveNoProductBai18()
        {
            return await _context.Suppliers
                .Where(x => x.Products.Count == 0)
                .ToListAsync();
        }

        [HttpGet] // Bai 19
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatGreaterThanAverageUnitPricesOfAllProductsBai19()
        {
            return await _context.Products
                 .Where(x => x.UnitPrice > _context.Products.Average(x => x.UnitPrice))
                 .ToListAsync();
        }

        public class GetAllNamesOfProductSupplierCategory
        {
            public string ProductName { get; set; } = null!;
            public string SupplierName { get; set; } = null!;
            public string CategoryName { get; set; } = null!;
            public GetAllNamesOfProductSupplierCategory(Product product)
            {
                ProductName = product.ProductName;
                SupplierName = product.Supplier!.CompanyName;
                CategoryName = product.Category!.CategoryName;
            }
        }
        [HttpGet] // Bai 20
        public async Task<ActionResult<IEnumerable<GetAllNamesOfProductSupplierCategory>>> GetAllNamesOfProductAndSupplierAndCategoryBai20()
        {
            return await _context.Products
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Select(x => new GetAllNamesOfProductSupplierCategory(x))
                .ToListAsync();
        }

        [HttpGet] // Bai 21
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsThatDiscontinuedAndUnitPriceGreaterThan50Bai21()
        {
            return await _context.Products
                .Where(x => x.Discontinued && x.UnitPrice > 50)
                .ToListAsync();
        }

        [HttpGet] // Bai 22
        public async Task<ActionResult<Category>> GetCategoryThatHaveMostProductsBai22()
        {
            return await _context.Categories
                .OrderByDescending(x => x.Products.Count)
                .FirstOrDefaultAsync();
        }

        [HttpGet] // Bai 23
        public async Task<ActionResult<Supplier>> GetSupplierThatHaveMostProductsBai23()
        {
            return await _context.Suppliers
                .OrderByDescending(x => x.Products.Count)
                .FirstOrDefaultAsync();
        }

        public class GetAllProductsWithSupplierTotalProductsCount
        {
            public Product products { get; set; }
            public int SupplierProductsCount { get; set; }
            public GetAllProductsWithSupplierTotalProductsCount(Product product, int count)
            {
                products = product;
                SupplierProductsCount = count;
            }
        }
        [HttpGet] // Bai 24
        public async Task<ActionResult<IEnumerable<GetAllProductsWithSupplierTotalProductsCount>>> GetAllProductsWithSupplierWhichIncludeTotalProductsCountBai24()
        {
            return await _context.Products
                .Select(x => new GetAllProductsWithSupplierTotalProductsCount(x, _context.Products.Count(y => y.SupplierId == x.SupplierId)))
                .ToListAsync();
        }
    }
}
