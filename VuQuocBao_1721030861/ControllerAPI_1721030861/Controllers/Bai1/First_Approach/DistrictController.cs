using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai1.First_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.First_Approach
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IRepository<District> _districtService;
        private readonly IMapper _mapper;

        public DistrictController(IRepository<District> districtService, IMapper mapper)
        {
            _districtService = districtService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictDTO>> Get(int id)
        {
            var entity = await _districtService.GetAsync(id);
            if (entity != null)
            {
                var model = new DistrictDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<District>> GetFull(int id)
        {
            return await _districtService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDTO>>> GetList()
        {
            var entityList = await _districtService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<DistrictDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDTO>>> Search(string txtSearch)
        {
            Expression<Func<District, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _districtService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<DistrictDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> SearchFull(string txtSearch)
        {
            Expression<Func<District, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _districtService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _districtService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DistrictDTO>> Create(DistrictDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _districtService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new District();
            _mapper.Map(model, newModel);
            newModel.CreatedAt = DateTime.Now;
            if (await _districtService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<DistrictDTO>> Update(DistrictDTO model)
        {
            if (_districtService.CheckExists(model.Id))
            {
                var entity = new District();
                _mapper.Map(model, entity);
                if (await _districtService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
