using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai2.Second_Approach
{
    [Route("bai2/[controller]/[action]")]
    [ApiController]
    public class FolkController : ControllerBase
    {
        private readonly IGenericRepository<Folk> _folkRepository;
        private readonly IMapper _mapper;

        public FolkController(IGenericRepository<Folk> schoolService, IMapper mapper)
        {
            _folkRepository = schoolService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<FolkDTO>> Get(int id)
        {
            var entity = await _folkRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new FolkDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Folk>> GetFull(int id)
        {
            return await _folkRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolkDTO>>> GetList()
        {
            var entityList = await _folkRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<FolkDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolkDTO>>> Search(string txtSearch)
        {
            Expression<Func<Folk, bool>> filter = a => a.Status != -1 && (a.Name!.Contains(txtSearch));
            var entityList = await _folkRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<FolkDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _folkRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _folkRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<FolkDTO>> Create(FolkDTO model)
        {
            Expression<Func<Folk, int>> filter = (x => x.Id);
            model.Id = await _folkRepository.MaxIdAsync(filter) + 1;

            var newModel = new Folk();
            _mapper.Map(model, newModel);

            if (await _folkRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<FolkDTO>> Update(FolkDTO model)
        {
            var entity = await _folkRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _folkRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
