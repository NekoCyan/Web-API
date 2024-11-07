using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Models;
using ControllerAPI_1721030861.Repositories.Second_Approach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Second_Approach
{
    [Route("[controller]/[action]")]
    [ApiController, Authorize]
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
        public async Task<ActionResult<APIResponse<NewsApiDTO>>> Get(int id)
        {
            var entity = await _newsRepository.GetAsync(id);
            if (entity == null) return NotFound(new APIResponse(404, "Not found"));

            var dto = new NewsApiDTO();
            _mapper.Map(entity, dto);
            return Ok(new APIResponse<NewsApiDTO>(dto));
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse<IEnumerable<NewsApiDTO>>>> GetList()
        {
            var entityList = await _newsRepository.GetListAsync();
            if (entityList == null) return NoContent();

            var dtoList = new List<NewsApiDTO>();
            _mapper.Map(entityList, dtoList);
            return Ok(new APIResponse<IEnumerable<NewsApiDTO>>(dtoList));
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse<IEnumerable<NewsApiDTO>>>> Search(string txtSearch)
        {
            Expression<Func<NewsApi, bool>> filter = a => a.Title!.Contains(txtSearch);
            var entityList = await _newsRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<NewsApiDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(new APIResponse<IEnumerable<NewsApiDTO>>(dtoList));
            }
            else
                return NoContent();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<APIResponse>> Create(NewsApiDTO model)
        {
            Expression<Func<NewsApi, int>> filter = x => x.Id;
            model.Id = await _newsRepository.MaxIdAsync(filter) + 1;

            var newModel = new NewsApi();
            _mapper.Map(model, newModel);

            if (await _newsRepository.CreateAsync(newModel) != null)
                return Ok(new APIResponse(model));
            else
                return NoContent();
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<APIResponse>> Update(NewsApiDTO model)
        {
            var entity = await _newsRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _newsRepository.UpdateAsync(entity) != null)
                    return Ok(new APIResponse(model));
            }
            return NotFound(new APIResponse(404, "Not found"));
        }

        [HttpDelete, Authorize]
        public ActionResult Delete(int id)
        {
            var entity = _newsRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _newsRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok(new APIResponse(200, "Record is deleted"));
            }
            return NotFound(new APIResponse(404, "Not found"));
        }
    }
}
