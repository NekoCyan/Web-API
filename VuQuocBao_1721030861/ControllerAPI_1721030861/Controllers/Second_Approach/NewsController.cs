using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.Second_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Second_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IGenericRepository<NewsApi> _newsRepository;
        private readonly IMapper _mapper;

        public NewsController(IGenericRepository<NewsApi> newsService, IMapper mapper)
        {
            _newsRepository = newsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<NewsApiDTO>> Get(int id)
        {
            var entity = await _newsRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new NewsApiDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<NewsApi>> GetFull(int id)
        {
            return await _newsRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsApiDTO>>> GetList()
        {
            var entityList = await _newsRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<NewsApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsApiDTO>>> Search(string txtSearch)
        {
            Expression<Func<NewsApi, bool>> filter = a => a.Title!.Contains(txtSearch);
            var entityList = await _newsRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<NewsApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _newsRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _newsRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<NewsApiDTO>> Create(NewsApiDTO model)
        {
            Expression<Func<NewsApi, int>> filter = x => x.Id;
            model.Id = await _newsRepository.MaxIdAsync(filter) + 1;

            var newModel = new NewsApi();
            _mapper.Map(model, newModel);

            if (await _newsRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<NewsApiDTO>> Update(NewsApiDTO model)
        {
            var entity = await _newsRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _newsRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
