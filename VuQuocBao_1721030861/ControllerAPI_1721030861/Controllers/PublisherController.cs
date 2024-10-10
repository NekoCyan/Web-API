using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IGenericRepository<Publisher> _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherController(IGenericRepository<Publisher> publisherService, IMapper mapper)
        {
            _publisherRepository = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PublisherDTO>> Get(int id)
        {
            var entity = await _publisherRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new PublisherDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Publisher>> GetFull(int id)
        {
            return await _publisherRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetList()
        {
            var entityList = await _publisherRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<PublisherDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> Search(string PublisherName)
        {
            Expression<Func<Publisher, bool>> filter = a => a.Status != -1 && (a.PubName.ToLower().Contains(PublisherName.ToLower()));
            var entityList = await _publisherRepository.SearchAsync(filter);
            if (entityList != null)
            {
                var dtoList = new List<PublisherDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _publisherRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _publisherRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDTO>> Create(PublisherDTO model)
        {
            Expression<Func<Publisher, int>> filter = (x => x.Id);
            model.Id = await _publisherRepository.MaxIdAsync(filter) + 1;

            var newModel = new Publisher();
            _mapper.Map(model, newModel);

            if (await _publisherRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<PublisherDTO>> Update(PublisherDTO model)
        {
            var entity = await _publisherRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _publisherRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
