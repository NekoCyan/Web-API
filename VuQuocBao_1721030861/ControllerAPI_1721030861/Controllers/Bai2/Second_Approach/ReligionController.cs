using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai2.Second_Approach
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReligionController : ControllerBase
    {
        private readonly IGenericRepository<Religion> _religionRepository;
        private readonly IMapper _mapper;

        public ReligionController(IGenericRepository<Religion> schoolService, IMapper mapper)
        {
            _religionRepository = schoolService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ReligionDTO>> Get(int id)
        {
            var entity = await _religionRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new ReligionDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Religion>> GetFull(int id)
        {
            return await _religionRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReligionDTO>>> GetList()
        {
            var entityList = await _religionRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<ReligionDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReligionDTO>>> Search(string txtSearch)
        {
            Expression<Func<Religion, bool>> filter = a => a.Status != -1 && (a.Name!.Contains(txtSearch));
            var entityList = await _religionRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<ReligionDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _religionRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _religionRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ReligionDTO>> Create(ReligionDTO model)
        {
            Expression<Func<Religion, int>> filter = (x => x.Id);
            model.Id = await _religionRepository.MaxIdAsync(filter) + 1;

            var newModel = new Religion();
            _mapper.Map(model, newModel);

            if (await _religionRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<ReligionDTO>> Update(ReligionDTO model)
        {
            var entity = await _religionRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _religionRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
