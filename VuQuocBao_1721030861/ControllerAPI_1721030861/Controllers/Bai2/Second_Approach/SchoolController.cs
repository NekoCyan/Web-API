using AutoMapper;
using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Repositories.Bai2.Second_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Bai2.Second_Approach
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IGenericRepository<School> _schoolRepository;
        private readonly IMapper _mapper;

        public SchoolController(IGenericRepository<School> schoolService, IMapper mapper)
        {
            _schoolRepository = schoolService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SchoolDTO>> Get(int id)
        {
            var entity = await _schoolRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new SchoolDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<School>> GetFull(int id)
        {
            return await _schoolRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDTO>>> GetList()
        {
            var entityList = await _schoolRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<SchoolDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDTO>>> Search(string txtSearch)
        {
            Expression<Func<School, bool>> filter = a => a.Status != -1 && (a.Name!.Contains(txtSearch));
            var entityList = await _schoolRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<SchoolDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _schoolRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _schoolRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SchoolDTO>> Create(SchoolDTO model)
        {
            Expression<Func<School, int>> filter = (x => x.Id);
            model.Id = await _schoolRepository.MaxIdAsync(filter) + 1;

            var newModel = new School();
            _mapper.Map(model, newModel);

            if (await _schoolRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<SchoolDTO>> Update(SchoolDTO model)
        {
            var entity = await _schoolRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _schoolRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
