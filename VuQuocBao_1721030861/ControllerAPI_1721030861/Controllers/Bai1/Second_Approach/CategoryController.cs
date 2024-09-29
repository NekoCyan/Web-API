using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach; // Shared Generic Repository
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.Second_Approach
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(IGenericRepository<Category> categoryService, IMapper mapper)
        {
            _categoryRepository = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var entity = await _categoryRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new CategoryDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Category>> GetFull(int id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetList()
        {
            var entityList = await _categoryRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<CategoryDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Search(string txtSearch)
        {
            Expression<Func<Category, bool>> filter = a => a.Status != -1 && (a.CategoryName!.Contains(txtSearch));
            var entityList = await _categoryRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<CategoryDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _categoryRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _categoryRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO model)
        {
            Expression<Func<Category, int>> filter = (x => x.Id);
            model.Id = await _categoryRepository.MaxIdAsync(filter) + 1;

            var newModel = new Category();
            _mapper.Map(model, newModel);

            if (await _categoryRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> Update(CategoryDTO model)
        {
            var entity = await _categoryRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _categoryRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
