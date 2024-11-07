using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.Simple;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI_1721030861.Controllers.Simple
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _cagtegoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService customerService, IMapper mapper)
        {
            _cagtegoryService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryApiDTO>> Get(int id)
        {
            var get = await _cagtegoryService.GetAsync(id);
            if (get == null) return NotFound();

            return _mapper.Map<CategoryApiDTO>(get);
        }

        [HttpGet]
        public async Task<ActionResult<CategoryApi>> GetFull(int id)
        {
            var get = await _cagtegoryService.GetAsync(id, false);
            if (get == null) return NotFound();

            return get;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryApiDTO>>> GetList()
        {
            var entityList = await _cagtegoryService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<CategoryApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryApiDTO model)
        {
            if (_cagtegoryService.CheckExists(model.Id))
            {
                var entity = new CategoryApi();
                _mapper.Map(model, entity);
                if (await _cagtegoryService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryApiDTO>> Create(CategoryApiDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _cagtegoryService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new CategoryApi();
            _mapper.Map(model, newModel);
            if (await _cagtegoryService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _cagtegoryService.Delete(id);
            if (result == 1)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
