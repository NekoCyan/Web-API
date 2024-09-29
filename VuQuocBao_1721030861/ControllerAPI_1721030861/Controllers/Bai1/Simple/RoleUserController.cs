using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai1.Simple;
using Microsoft.AspNetCore.Mvc;

namespace ControllerAPI_1721030861.Controllers.Bai1.Simple
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly IRoleUserService _roleUserService;
        private readonly IMapper _mapper;

        public RoleUserController(IRoleUserService roleUserService, IMapper mapper)
        {
            _roleUserService = roleUserService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RoleUserDTO>> Get(int id)
        {
            return _mapper.Map<RoleUserDTO>(await _roleUserService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<RoleUser>> GetFull(int id)
        {
            return await _roleUserService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleUserDTO>>> GetList()
        {
            var entityList = await _roleUserService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<RoleUserDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUserDTO model)
        {
            if (_roleUserService.CheckExists(model.Id))
            {
                var entity = new RoleUser();
                _mapper.Map(model, entity);
                if (await _roleUserService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RoleUserDTO>> Create(RoleUserDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _roleUserService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new RoleUser();
            _mapper.Map(model, newModel);
            if (await _roleUserService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _roleUserService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
