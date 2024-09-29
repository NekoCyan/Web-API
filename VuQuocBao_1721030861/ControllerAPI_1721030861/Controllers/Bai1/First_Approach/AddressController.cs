using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai1.First_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.First_Approach
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IRepository<Address> _addressService;
        private readonly IMapper _mapper;

        public AddressController(IRepository<Address> addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDTO>> Get(int id)
        {
            var entity = await _addressService.GetAsync(id);
            if (entity != null)
            {
                var model = new AddressDTO();
                _mapper.Map(entity, model);
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<Address>> GetFull(int id)
        {
            return await _addressService.GetAsync(id, false);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetList()
        {
            var entityList = await _addressService.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<AddressDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> Search(string txtSearch)
        {
            Expression<Func<Address, bool>> filter = a => a.Status != -1 && a.AddressText!.Contains(txtSearch);
            var entityList = await _addressService.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<AddressDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> SearchFull(string txtSearch)
        {
            Expression<Func<Address, bool>> filter = a => a.Status != -1 && a.AddressText!.Contains(txtSearch);
            var entityList = await _addressService.SearchAsync(filter, false);
            if (entityList != null)
            {
                return Ok(entityList);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _addressService.Delete(id);
            if (result == 1)
            {
                return Ok(new { success = true, message = "Record is deleted." });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>> Create(AddressDTO model)
        {
            // Get Max Id in table of Database --> set for model + 1
            model.Id = await _addressService.MaxIdAsync(model.Id) + 1;

            //Mapp data model --> newModel
            var newModel = new Address();
            _mapper.Map(model, newModel);
            newModel.CreatedAt = DateTime.Now;
            if (await _addressService.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<AddressDTO>> Update(AddressDTO model)
        {
            if (_addressService.CheckExists(model.Id))
            {
                var entity = new Address();
                _mapper.Map(model, entity);
                if (await _addressService.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
