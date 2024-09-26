using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Repositories.Bai2.Simple;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai2.Simple
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankTypeController : ControllerBase
    {
        private readonly IBankTypeService _bankTypeService;
        private readonly IMapper _mapper;

        public BankTypeController(IBankTypeService bankTypeService, IMapper mapper)
        {
            _bankTypeService = bankTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<BankTypeDTO>> Get(int id)
        {
            return _mapper.Map<BankTypeDTO>(await _bankTypeService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<BankType>> GetFull(int id)
        {
            return await _bankTypeService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankTypeDTO>>> GetList()
        {
            var entityList = await _bankTypeService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<BankTypeDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankTypeDTO>>> Search(string txtSearch)
        {
            Expression<Func<BankType, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _bankTypeService.SearchAsync(filter, true);
            if (entityList != null)
            {
                var dtoList = new List<BankTypeDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankType>>> SearchFull(string txtSearch)
        {
            Expression<Func<BankType, bool>> filter;
            filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _bankTypeService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BankTypeDTO model)
        {
            if (_bankTypeService.CheckExists(model.Id))
            {
                var entity = new BankType();
                _mapper.Map(model, entity);
                if (await _bankTypeService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<BankTypeDTO>> Create(BankTypeDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _bankTypeService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new BankType();
            _mapper.Map(model, newModel);
            if (await _bankTypeService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _bankTypeService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
