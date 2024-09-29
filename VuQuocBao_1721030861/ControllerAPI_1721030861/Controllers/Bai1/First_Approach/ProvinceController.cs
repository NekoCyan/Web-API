using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai1.First_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.First_Approach
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IRepository<Province> _provinceService;
        private readonly IMapper _mapper;

        public ProvinceController(IRepository<Province> provinceService, IMapper mapper)
        {
            _provinceService = provinceService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProvinceDTO>> Get(int id)
        {
            var entity = await _provinceService.GetAsync(id);
            if (entity != null)
            {
                var model = new ProvinceDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<Province>> GetFull(int id)
        {
            return await _provinceService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> GetList()
        {
            var entityList = await _provinceService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<ProvinceDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> Search(string txtSearch)
        {
            Expression<Func<Province, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _provinceService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<ProvinceDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> SearchFull(string txtSearch)
        {
            Expression<Func<Province, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _provinceService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _provinceService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProvinceDTO>> Create(ProvinceDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _provinceService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Province();
            _mapper.Map(model, newModel);
            newModel.CreatedAt = DateTime.Now;
            if (await _provinceService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<ProvinceDTO>> Update(ProvinceDTO model)
        {
            if (_provinceService.CheckExists(model.Id))
            {
                var entity = new Province();
                _mapper.Map(model, entity);
                if (await _provinceService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
