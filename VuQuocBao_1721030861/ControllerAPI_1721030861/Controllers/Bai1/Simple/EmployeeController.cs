using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai1.Simple;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.Simple
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {
            return _mapper.Map<EmployeeDTO>(await _employeeService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> GetFull(int id)
        {
            return await _employeeService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetList()
        {
            var entityList = await _employeeService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<EmployeeDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Search(string txtSearch)
        {
            Expression<Func<Employee, bool>> filter;
            filter = a => a.Status != -1 && (a.FirstName!.Contains(txtSearch) || a.LastName!.Contains(txtSearch));
            var entityList = await _employeeService.SearchAsync(filter, true);
            if (entityList != null)
            {
                var dtoList = new List<EmployeeDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchFull(string txtSearch)
        {
            Expression<Func<Employee, bool>> filter;
            filter = a => a.Status != -1 && (a.FirstName!.Contains(txtSearch) || a.LastName!.Contains(txtSearch));
            var entityList = await _employeeService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeDTO model)
        {
            if (_employeeService.CheckExists(model.Id))
            {
                var entity = new Employee();
                _mapper.Map(model, entity);
                if (await _employeeService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Create(EmployeeDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _employeeService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Employee();
            _mapper.Map(model, newModel);
            if (await _employeeService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _employeeService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
