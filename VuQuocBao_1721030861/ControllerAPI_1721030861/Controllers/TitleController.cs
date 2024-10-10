using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly MidTermTestApiContext _context;
        private readonly IGenericRepository<Title> _titleRepository;
        private readonly IMapper _mapper;

        public TitleController(MidTermTestApiContext context, IGenericRepository<Title> titleService, IMapper mapper)
        {
            _context = context;
            _titleRepository = titleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TitleDTO>> Get(int id)
        {
            var entity = await _titleRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new TitleDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Title>> GetFull(int id)
        {
            return await _titleRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TitleDTO>>> GetList()
        {
            var entityList = await _titleRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<TitleDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TitleDTO>>> Search(string TitleName)
        {
            Expression<Func<Title, bool>> filter = a => a.Status != -1 && (a.TitleName.ToLower().Contains(TitleName.ToLower()));
            var entityList = await _titleRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<TitleDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _titleRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _titleRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<TitleDTO>> Create(TitleDTO model)
        {
            if (!await _context.Publishers.AnyAsync(x => x.Id == model.PubId))
            {
                return NotFound($"Publisher ID {model.PubId} was not found.");
            }

            Expression<Func<Title, int>> filter = (x => x.Id);
            model.Id = await _titleRepository.MaxIdAsync(filter) + 1;

            var newModel = new Title();
            _mapper.Map(model, newModel);

            if (await _titleRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<TitleDTO>> Update(TitleDTO model)
        {
            if (!await _context.Publishers.AnyAsync(x => x.Id == model.PubId))
            {
                return NotFound($"Publisher ID {model.PubId} was not found.");
            }

            var entity = await _titleRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _titleRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
