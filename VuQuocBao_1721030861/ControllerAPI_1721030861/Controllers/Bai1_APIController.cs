using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bai1_APIController : ControllerBase
    {
        private readonly EFCoreDemoContext _context;
        private readonly IMapper _mapper;
        public Bai1_APIController(EFCoreDemoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var Products = await _context.Products
                .Select(Product => _mapper.Map<ProductDTO>(Product))
                .ToListAsync();
            return Products;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateNewProduct(ProductDTO Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsSupplierExists(Product.SupplierId))
            {
                ModelState.AddModelError("SupplierId", "Invalid SupplierId");
                return NotFound(ModelState);
            }
            if (!_context.IsCategoryExists(Product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                return NotFound(ModelState);
            }

            var NewProduct = _mapper.Map<Product>(Product);
            _context.Products.Add(NewProduct);

            await _context.SaveChangesAsync();

            return NewProduct;
        }

        [HttpPut]
        public async Task<ActionResult<Product>> EditProduct(int ProductId, ProductDTO Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsProductExists(ProductId))
            {
                ModelState.AddModelError("ProductId", "Invalid ProductId");
                return NotFound(ModelState);
            }
            if (!_context.IsSupplierExists(Product.SupplierId))
            {
                ModelState.AddModelError("SupplierId", "Invalid SupplierId");
                return NotFound(ModelState);
            }
            if (!_context.IsCategoryExists(Product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                return NotFound(ModelState);
            }

            var UpdateProduct = _mapper.Map<Product>(Product);
            UpdateProduct.ProductId = ProductId;

            _context.Products.Update(UpdateProduct);

            await _context.SaveChangesAsync();

            return UpdateProduct;
        }

        [HttpDelete]
        public async Task<ActionResult<Product>> DeleteProduct(int ProductId)
        {
            var Product = await _context.Products.FindAsync(ProductId);
            if (Product is null)
            {
                ModelState.AddModelError("ProductId", "Invalid ProductId");
                return NotFound();
            }

            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();

            return Product;
        }

        [HttpGet]
        [Route("{ProductId}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int ProductId)
        {
            var Product = await _context.Products.FindAsync(ProductId);
            if (Product is null)
            {
                ModelState.AddModelError("ProductId", "Invalid ProductId");
                return NotFound(ModelState);
            }
            return _mapper.Map<ProductDTO>(Product);
        }

        [HttpGet]
        [Route("FilterByProductName")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FilterByProductName(string ProductName)
        {
            var Products = await _context.Products
                .Where(Product => Product.ProductName.Contains(ProductName))
                .Select(Product => _mapper.Map<ProductDTO>(Product))
                .ToListAsync();
            return Products;
        }
    }
}
