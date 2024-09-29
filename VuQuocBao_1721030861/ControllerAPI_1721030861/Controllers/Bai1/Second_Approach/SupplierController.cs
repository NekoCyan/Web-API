using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach; // Shared Generic Repository
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.Second_Approach
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IGenericRepository<Supplier> _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierController(IGenericRepository<Supplier> supplierService, IMapper mapper)
        {
            _supplierRepository = supplierService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SupplierDTO>> Get(int id)
        {
            var entity = await _supplierRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new SupplierDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Supplier>> GetFull(int id)
        {
            return await _supplierRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetList()
        {
            var entityList = await _supplierRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<SupplierDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> Search(string txtSearch)
        {
            Expression<Func<Supplier, bool>> filter = a => a.Status != -1 && (a.CompanyName!.Contains(txtSearch));
            var entityList = await _supplierRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<SupplierDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _supplierRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _supplierRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> Create(SupplierDTO model)
        {
            Expression<Func<Supplier, int>> filter = (x => x.Id);
            model.Id = await _supplierRepository.MaxIdAsync(filter) + 1;

            var newModel = new Supplier();
            _mapper.Map(model, newModel);

            if (await _supplierRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<SupplierDTO>> Update(SupplierDTO model)
        {
            var entity = await _supplierRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _supplierRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
