using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Repositories.Bai2.Simple;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai2.Simple
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly IMapper _mapper;

        public BankController(IBankService bankService, IMapper mapper)
        {
            _bankService = bankService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<BankDTO>> Get(int id)
        {
            return _mapper.Map<BankDTO>(await _bankService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<Bank>> GetFull(int id)
        {
            return await _bankService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDTO>>> GetList()
        {
            var entityList = await _bankService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<BankDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDTO>>> Search(string txtSearch)
        {
            Expression<Func<Bank, bool>> filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _bankService.SearchAsync(filter, true);
            if (entityList != null)
            {
                var dtoList = new List<BankDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> SearchFull(string txtSearch)
        {
            Expression<Func<Bank, bool>> filter;
            filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _bankService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BankDTO model)
        {
            if (_bankService.CheckExists(model.Id))
            {
                var entity = new Bank();
                _mapper.Map(model, entity);
                if (await _bankService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<BankDTO>> Create(BankDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _bankService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Bank();
            _mapper.Map(model, newModel);
            if (await _bankService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _bankService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
