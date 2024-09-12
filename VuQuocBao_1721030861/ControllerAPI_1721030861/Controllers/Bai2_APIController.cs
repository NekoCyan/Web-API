using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bai2_APIController : ControllerBase
    {
        private readonly EcommerceAddressContext _context;
        private readonly IMapper _mapper;
        public Bai2_APIController(EcommerceAddressContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAllAddresses()
        {
            var addresses = await _context.Addresses
                .Select(address => _mapper.Map<AddressDTO>(address))
                .ToListAsync();

            return addresses;
        }
    }
}
