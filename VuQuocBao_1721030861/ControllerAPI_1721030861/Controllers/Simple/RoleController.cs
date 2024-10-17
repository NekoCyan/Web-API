using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.Simple;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Simple
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RoleDTO>> Get(int id)
        {
            return _mapper.Map<RoleDTO>(await _roleService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<Role>> GetFull(int id)
        {
            return await _roleService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetList()
        {
            var entityList = await _roleService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<RoleDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> Search(string txtSearch)
        {
            Expression<Func<Role, bool>> filter;
            filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _roleService.SearchAsync(filter, true);
            if (entityList != null)
            {
                var dtoList = new List<RoleDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> SearchFull(string txtSearch)
        {
            Expression<Func<Role, bool>> filter;
            filter = a => a.Status != -1 && a.Name!.Contains(txtSearch);
            var entityList = await _roleService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleDTO model)
        {
            if (_roleService.CheckExists(model.Id))
            {
                var entity = new Role();
                _mapper.Map(model, entity);
                if (await _roleService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> Create(RoleDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _roleService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Role();
            _mapper.Map(model, newModel);
            if (await _roleService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _roleService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
