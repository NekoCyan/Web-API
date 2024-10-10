using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IGenericRepository<Author> authorService, IMapper mapper)
        {
            _authorRepository = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AuthorDTO>> Get(int id)
        {
            var entity = await _authorRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new AuthorDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Author>> GetFull(int id)
        {
            return await _authorRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetList()
        {
            var entityList = await _authorRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<AuthorDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> Search(string AuthorName)
        {
            Expression<Func<Author, bool>> filter =
                a => a.Status != -1 && (
                    a.AuLname!.ToLower().Contains(AuthorName.ToLower()) ||
                    a.AuFname!.ToLower().Contains(AuthorName.ToLower()) ||
                    AuthorName.ToLower().Contains(a.AuFname.ToLower()) ||
                    AuthorName.ToLower().Contains(a.AuLname.ToLower())
                );
            var entityList = await _authorRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<AuthorDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _authorRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _authorRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Create(AuthorDTO model)
        {
            Expression<Func<Author, int>> filter = (x => x.Id);
            model.Id = await _authorRepository.MaxIdAsync(filter) + 1;

            var newModel = new Author();
            _mapper.Map(model, newModel);

            if (await _authorRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<AuthorDTO>> Update(AuthorDTO model)
        {
            var entity = await _authorRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _authorRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
