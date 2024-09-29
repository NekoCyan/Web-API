using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai1;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach; // Shared Generic Repository
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai1.Second_Approach
{
    [Route("bai1/[controller]/[action]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IGenericRepository<Shipper> _shipperRepository;
        private readonly IMapper _mapper;

        public ShipperController(IGenericRepository<Shipper> shipperService, IMapper mapper)
        {
            _shipperRepository = shipperService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ShipperDTO>> Get(int id)
        {
            var entity = await _shipperRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new ShipperDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Shipper>> GetFull(int id)
        {
            return await _shipperRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDTO>>> GetList()
        {
            var entityList = await _shipperRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<ShipperDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDTO>>> Search(string txtSearch)
        {
            Expression<Func<Shipper, bool>> filter = a => a.Status != -1 && (a.CompanyName!.Contains(txtSearch));
            var entityList = await _shipperRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<ShipperDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _shipperRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _shipperRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ShipperDTO>> Create(ShipperDTO model)
        {
            Expression<Func<Shipper, int>> filter = (x => x.Id);
            model.Id = await _shipperRepository.MaxIdAsync(filter) + 1;

            var newModel = new Shipper();
            _mapper.Map(model, newModel);

            if (await _shipperRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<ShipperDTO>> Update(ShipperDTO model)
        {
            var entity = await _shipperRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _shipperRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
