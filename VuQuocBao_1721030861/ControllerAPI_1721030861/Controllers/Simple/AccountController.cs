using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Models;
using ControllerAPI_1721030861.Repositories.Simple;
using ControllerAPI_1721030861.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Simple
{
    [Route("[controller]/[action]"), ApiController]
    //[ValidateAntiForgeryToken]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly Authentication _authentication;

        public AccountController(IAccountService accountService, IMapper mapper, Authentication authentication)
        {
            _accountService = accountService;
            _mapper = mapper;
            _authentication = authentication;
        }

        [HttpPost, EnableCors("AllowAll")]
        public ActionResult<APIResponse<string>> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) return Unauthorized(new APIResponse<string>(-1, "Invalid model state"));

            if (model.userName == "adminadmin" && model.password == "Adminadmin123")
            {
                return Ok(new APIResponse<string>(1, "OK", "Nothing"));
            }

            return Unauthorized(new APIResponse<string>(-1, "Invalid username or password"));
        }

        [HttpGet, EnableCors("AllowAll")]
        //[Authorize] // Disable this to test token
        public async Task<ActionResult<string>> GetLoginTokenById(int id)
        {
            var account = await _accountService.GetAsync(id);
            if (account is null) return NotFound();

            return Ok(new APIResponse<string>(1, "OK", _authentication.GenerateAccessToken(account)));
        }

        [HttpGet]
        public async Task<ActionResult<string>> RefreshToken(string CurrentToken)
        {
            if (!_authentication.IsTokenValid(CurrentToken)) return Unauthorized(new APIResponse<string>(-1, "Token is Invalid"));

            return await _authentication.RefreshAccessToken(CurrentToken);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> Get(int id)
        {
            return _mapper.Map<AccountDTO>(await _accountService.GetAsync(id));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> GetFull(int id)
        {
            return await _accountService.GetAsync(id, false);
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Update([FromBody] AccountDTO model)
        {
            if (!ModelState.IsValid) return Unauthorized(new APIResponse<string>(-1, "Invalid model state"));

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
        public async Task<ActionResult<AccountDTO>> Create([FromBody] AccountDTO model)
        {
            if (!ModelState.IsValid) return Unauthorized(new APIResponse<string>(-1, "Invalid model state"));

            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _accountService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Account();
            newModel.Password = "123456"; // Just for example.
            _mapper.Map(model, newModel);
            if (await _accountService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        [Authorize]
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
