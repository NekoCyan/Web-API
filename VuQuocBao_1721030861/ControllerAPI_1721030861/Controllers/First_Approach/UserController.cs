using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.First_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.First_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<UserApi> _userService;
        private readonly IMapper _mapper;

        public UserController(IRepository<UserApi> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserApiDTO>> Get(int id)
        {
            var entity = await _userService.GetAsync(id);
            if (entity != null)
            {
                var model = new UserApiDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<UserApi>> GetFull(int id)
        {
            return await _userService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserApiDTO>>> GetList()
        {
            var entityList = await _userService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<UserApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserApiDTO>>> Search(string txtSearch)
        {
            Expression<Func<UserApi, bool>> filter = a => a.UserName!.Contains(txtSearch);
            var entityList = await _userService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<UserApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserApi>>> SearchFull(string txtSearch)
        {
            Expression<Func<UserApi, bool>> filter = a => a.UserName!.Contains(txtSearch);
            var entityList = await _userService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            if (result == 1)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserApiDTO>> Create(UserApiDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _userService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new UserApi();
            _mapper.Map(model, newModel);
            if (await _userService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<UserApiDTO>> Update(UserApiDTO model)
        {
            if (_userService.CheckExists(model.Id))
            {
                var entity = new UserApi();
                _mapper.Map(model, entity);
                if (await _userService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
