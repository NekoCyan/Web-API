using AutoMapper;
using ControllerAPI_1721030861.Database.Models;
using ControllerAPI_1721030861.Repositories.Second_Approach;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Controllers.Second_Approach
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IGenericRepository<Order> orderService, IMapper mapper)
        {
            _orderRepository = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            var entity = await _orderRepository.GetAsync(id);
            if (entity != null)
            {
                var dto = new OrderDTO();
                _mapper.Map(entity, dto);
                return Ok(dto);
            }
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Order>> GetFull(int id)
        {
            return await _orderRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetList()
        {
            var entityList = await _orderRepository.GetListAsync();
            if (entityList != null)
            {
                var dtoList = new List<OrderDTO>();
                _mapper.Map(entityList, dtoList);
                return Ok(dtoList);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var entity = _orderRepository.GetAsync(id);
            if (entity != null)
            {
                var result = _orderRepository.Delete(entity.Result);
                if (result > 0)
                    return Ok("Record is deleted");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(OrderDTO model)
        {
            Expression<Func<Order, int>> filter = x => x.Id;
            model.Id = await _orderRepository.MaxIdAsync(filter) + 1;

            var newModel = new Order();
            _mapper.Map(model, newModel);

            if (await _orderRepository.CreateAsync(newModel) != null)
                return Ok(model);
            else
                return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<OrderDTO>> Update(OrderDTO model)
        {
            var entity = await _orderRepository.GetAsync(model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                if (await _orderRepository.UpdateAsync(entity) != null)
                    return Ok(model);
            }
            return NotFound();
        }
    }
}
