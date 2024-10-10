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
    public class TitleAuthorController : ControllerBase
    {
        private readonly MidTermTestApiContext _context;
        private readonly IGenericRepository<TitleAuthor> _titleAuthorRepository;
        private readonly IMapper _mapper;

        public TitleAuthorController(MidTermTestApiContext context, IGenericRepository<TitleAuthor> titleAuthorService, IMapper mapper)
        {
            _context = context;
            _titleAuthorRepository = titleAuthorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TitleAuthorDTO>> Get(int id)
        {
            var entity = await _titleAuthorRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new TitleAuthorDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<TitleAuthor>> GetFull(int id)
        {
            return await _titleAuthorRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TitleAuthorDTO>>> GetList()
        {
            var entityList = await _titleAuthorRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<TitleAuthorDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _titleAuthorRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _titleAuthorRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<TitleAuthorDTO>> Create(TitleAuthorDTO model)
        {
            if (!await _context.Authors.AnyAsync(x => x.Id == model.AuthorId))
            {
                return NotFound($"Author ID {model.AuthorId} was not found.");
            }

            if (!await _context.Titles.AnyAsync(x => x.Id == model.TitleId))
            {
                return NotFound($"Title ID {model.TitleId} was not found.");
            }

            Expression<Func<TitleAuthor, int>> filter = (x => x.Id);
            model.Id = await _titleAuthorRepository.MaxIdAsync(filter) + 1;

            var newModel = new TitleAuthor();
            _mapper.Map(model, newModel);

            if (await _titleAuthorRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<TitleAuthorDTO>> Update(TitleAuthorDTO model)
        {
            if (!await _context.Authors.AnyAsync(x => x.Id == model.AuthorId))
            {
                return NotFound($"Author ID {model.AuthorId} was not found.");
            }

            if (!await _context.Titles.AnyAsync(x => x.Id == model.TitleId))
            {
                return NotFound($"Title ID {model.TitleId} was not found.");
            }

            var entity = await _titleAuthorRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _titleAuthorRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
