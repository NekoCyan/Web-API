using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.Second_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Second_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailController(IGenericRepository<OrderDetail> orderDetailService, IMapper mapper)
        {
            _orderDetailRepository = orderDetailService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OrderDetailDTO>> Get(int id)
        {
            var entity = await _orderDetailRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new OrderDetailDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<OrderDetail>> GetFull(int id)
        {
            return await _orderDetailRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetList()
        {
            var entityList = await _orderDetailRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<OrderDetailDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _orderDetailRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _orderDetailRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailDTO>> Create(OrderDetailDTO model)
        {
            Expression<Func<OrderDetail, int>> filter = x => x.Id;
            model.Id = await _orderDetailRepository.MaxIdAsync(filter) + 1;

            var newModel = new OrderDetail();
            _mapper.Map(model, newModel);

            if (await _orderDetailRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<OrderDetailDTO>> Update(OrderDetailDTO model)
        {
            var entity = await _orderDetailRepository.GetAsync(model.OrderId);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _orderDetailRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
