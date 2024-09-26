using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Repositories.Bai2.First_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai2.First_Approach
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IRepository<Country> _countryService;
        private readonly IMapper _mapper;

        public CountryController(IRepository<Country> countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> Get(int id)
        {
            var entity = await _countryService.GetAsync(id);
            if (entity != null)
            {
                var model = new CountryDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<Country>> GetFull(int id)
        {
            return await _countryService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetList()
        {
            var entityList = await _countryService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<CountryDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> Search(string txtSearch)
        {
            Expression<Func<Country, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _countryService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<CountryDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> SearchFull(string txtSearch)
        {
            Expression<Func<Country, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _countryService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _countryService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CountryDTO>> Create(CountryDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _countryService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Country();
            _mapper.Map(model, newModel);
            newModel.CreatedAt = DateTime.Now;
            if (await _countryService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<CountryDTO>> Update(CountryDTO model)
        {
            if (_countryService.CheckExists(model.Id))
            {
                var entity = new Country();
                _mapper.Map(model, entity);
                if (await _countryService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
