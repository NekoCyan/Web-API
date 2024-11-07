using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Models;
using ControllerAPI_1721030861.Repositories.First_Approach;
using ControllerAPI_1721030861.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.First_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<UserApi> _userService;
        private readonly IMapper _mapper;
        private readonly FinalExamApiContext _context;
        private readonly Authentication _authentication;

        public UserController(IRepository<UserApi> userService, IMapper mapper, FinalExamApiContext context, Authentication authentication)
        {
            _userService = userService;
            _mapper = mapper;
            _context = context;
            _authentication = authentication;
        }

        [HttpPost, EnableRateLimiting("Account"), AllowAnonymous]
        public async Task<ActionResult<APIResponse<string>>> Login([FromBody] LoginModel login)
        {
            var resOnFailed = new APIResponse(401, "Invalid username or password");
            var user = await _context.UserApis.FirstOrDefaultAsync(x => x.UserName == login.userName);
            if (
                user == null ||
                !user.Password.Equals(login.password) // Due to no encrypt/decrypt password on db so simple check
            ) return Unauthorized(resOnFailed);

            return new APIResponse<string>(_authentication.GenerateAccessToken(user));
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<APIResponse<UserApiDTO>>> Get(int id)
        {
            var entity = await _userService.GetAsync(id);
            if (entity == null) return NotFound(new APIResponse(404, "Not found"));

            var model = new UserApiDTO();
            _mapper.Map(entity, model);
            return Ok(new APIResponse<UserApiDTO>(model));
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<APIResponse<UserApi>>> GetFull(int id)
        {
            var entity = await _userService.GetAsync(id, false);
            if (entity == null) return NotFound(new APIResponse(404, "Not found"));

            return Ok(new APIResponse<UserApi>(entity));
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<APIResponse<IEnumerable<UserApiDTO>>>> GetList()
        {
            var entityList = await _userService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<UserApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(new APIResponse<IEnumerable<UserApiDTO>>(dtoList));
            }
            return NoContent();
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<APIResponse<IEnumerable<UserApiDTO>>>> Search(string txtSearch)
        {
            Expression<Func<UserApi, bool>> filter = a => a.UserName!.Contains(txtSearch);
            var entityList = await _userService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<UserApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(new APIResponse<IEnumerable<UserApiDTO>>(dtoList));
            }
            return NoContent();
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<APIResponse<IEnumerable<UserApi>>>> SearchFull(string txtSearch)
        {
            Expression<Func<UserApi, bool>> filter = a => a.UserName!.Contains(txtSearch);
            var entityList = await _userService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(new APIResponse<IEnumerable<UserApi>>(entityList));
            }
            return NoContent();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<APIResponse>> Create(UserApiDTOWithPasswrd model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _userService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new UserApi();
            _mapper.Map(model, newModel);
            if (await _userService.CreateAsync(newModel) != null)
                return Ok(new APIResponse(model));
            else
                return NoContent();
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<APIResponse>> Update(UserApiDTOWithPasswrd model)
        {
            if (_userService.CheckExists(model.Id))
            {
                var entity = new UserApi();
                _mapper.Map(model, entity);
                if (await _userService.UpdateAsync(entity) != null)
                    return Ok(new APIResponse(model));
            }
            return NotFound(new APIResponse(404, "Not found"));
        }

        [HttpDelete, Authorize]
        public ActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            if (result == 1)
            {
                return Ok(new APIResponse(200, "Record is deleted"));
            }
            return NotFound(new APIResponse(404, "Not found"));
        }
    }
}
