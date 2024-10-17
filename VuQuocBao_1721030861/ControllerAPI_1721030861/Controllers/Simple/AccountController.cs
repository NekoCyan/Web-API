using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.Simple;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Simple
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AccountDTO>> Get(int id)
        {
            return _mapper.Map<AccountDTO>(await _accountService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<Account>> GetFull(int id)
        {
            return await _accountService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetList()
        {
            var entityList = await _accountService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<AccountDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> Search(string txtSearch)
        {
            Expression<Func<Account, bool>> filter = a => a.Status != -1 && a.UserName!.Contains(txtSearch);
            var entityList = await _accountService.SearchAsync(filter, true);
            if (entityList != null)
            {
                var dtoList = new List<AccountDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> SearchFull(string txtSearch)
        {
            Expression<Func<Account, bool>> filter;
            filter = a => a.Status != -1 && a.UserName!.Contains(txtSearch);
            var entityList = await _accountService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(AccountDTO model)
        {
            if (_accountService.CheckExists(model.Id))
            {
                var entity = new Account();
                _mapper.Map(model, entity);
                if (await _accountService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AccountDTO>> Create(AccountDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _accountService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Account();
            _mapper.Map(model, newModel);
            if (await _accountService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _accountService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
