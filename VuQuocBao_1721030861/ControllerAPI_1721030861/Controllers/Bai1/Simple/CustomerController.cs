using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai1.Simple;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.Simple
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerDTO>> Get(int id)
        {
            return _mapper.Map<CustomerDTO>(await _customerService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetFull(int id)
        {
            return await _customerService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetList()
        {
            var entityList = await _customerService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<CustomerDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> Search(string txtSearch)
        {
            Expression<Func<Customer, bool>> filter = a => a.Status != -1 && a.CompanyName!.Contains(txtSearch);
            var entityList = await _customerService.SearchAsync(filter, true);
            if (entityList != null)
            {
                var dtoList = new List<CustomerDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchFull(string txtSearch)
        {
            Expression<Func<Customer, bool>> filter;
            filter = a => a.Status != -1 && a.CompanyName!.Contains(txtSearch);
            var entityList = await _customerService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerDTO model)
        {
            if (_customerService.CheckExists(model.Id))
            {
                var entity = new Customer();
                _mapper.Map(model, entity);
                if (await _customerService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create(CustomerDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _customerService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Customer();
            _mapper.Map(model, newModel);
            if (await _customerService.CreateAsync(newModel) != null)
                return Ok(model);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _customerService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }
    }
}
