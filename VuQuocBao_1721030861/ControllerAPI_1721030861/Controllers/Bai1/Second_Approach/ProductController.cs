using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach; // Shared Generic Repository
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.Second_Approach
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productService, IMapper mapper)
        {
            _productRepository = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var entity = await _productRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new ProductDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetFull(int id)
        {
            return await _productRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetList()
        {
            var entityList = await _productRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<ProductDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Search(string txtSearch)
        {
            Expression<Func<Product, bool>> filter = a => a.Status != -1 && (a.ProductName!.Contains(txtSearch));
            var entityList = await _productRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<ProductDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _productRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _productRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO model)
        {
            Expression<Func<Product, int>> filter = (x => x.Id);
            model.Id = await _productRepository.MaxIdAsync(filter) + 1;

            var newModel = new Product();
            _mapper.Map(model, newModel);

            if (await _productRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Update(ProductDTO model)
        {
            var entity = await _productRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _productRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
