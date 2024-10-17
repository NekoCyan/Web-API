using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.First_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.First_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WardController : ControllerBase
    {
        private readonly IRepository<Ward> _wardService;
        private readonly IMapper _mapper;

        public WardController(IRepository<Ward> wardService, IMapper mapper)
        {
            _wardService = wardService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WardDTO>> Get(int id)
        {
            var entity = await _wardService.GetAsync(id);
            if (entity != null)
            {
                var model = new WardDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<Ward>> GetFull(int id)
        {
            return await _wardService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardDTO>>> GetList()
        {
            var entityList = await _wardService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<WardDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardDTO>>> Search(string txtSearch)
        {
            Expression<Func<Ward, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _wardService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<WardDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> SearchFull(string txtSearch)
        {
            Expression<Func<Ward, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _wardService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _wardService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<WardDTO>> Create(WardDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _wardService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Ward();
            _mapper.Map(model, newModel);
            newModel.CreatedAt = DateTime.Now;
            if (await _wardService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<WardDTO>> Update(WardDTO model)
        {
            if (_wardService.CheckExists(model.Id))
            {
                var entity = new Ward();
                _mapper.Map(model, entity);
                if (await _wardService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
