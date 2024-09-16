using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
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

        [HttpPost]
        public async Task<ActionResult<Address>> CreateNewAddress(AddressDTO Address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsCountryExists(Address.CountryId))
            {
                ModelState.AddModelError("CountryId", "Invalid CountryId");
                return NotFound(ModelState);
            }
            if (!_context.IsProvinceExists(Address.ProvinceId))
            {
                ModelState.AddModelError("ProvinceId", "Invalid ProvinceId");
                return NotFound(ModelState);
            }
            if (!_context.IsDistrictExists(Address.DistrictId))
            {
                ModelState.AddModelError("DistrictId", "Invalid DistrictId");
                return NotFound(ModelState);
            }
            if (!_context.IsWardExists(Address.WardId))
            {
                ModelState.AddModelError("WardId", "Invalid WardId");
                return NotFound(ModelState);
            }

            var NewAddress = _mapper.Map<Address>(Address);

            var newCountryId = _context.Countries.Max(x => x.CountryId) + 1;
            NewAddress.AddressId = newCountryId;

            _context.Addresses.Add(NewAddress);

            await _context.SaveChangesAsync();

            return NewAddress;
        }

        [HttpPut]
        public async Task<ActionResult<Address>> EditAddress(int AddressId, AddressDTO Address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsAddressExists(AddressId))
            {
                ModelState.AddModelError("AddressId", "Invalid AddressId");
                return NotFound(ModelState);
            }
            if (!_context.IsCountryExists(Address.CountryId))
            {
                ModelState.AddModelError("CountryId", "Invalid CountryId");
                return NotFound(ModelState);
            }
            if (!_context.IsProvinceExists(Address.ProvinceId))
            {
                ModelState.AddModelError("ProvinceId", "Invalid ProvinceId");
                return NotFound(ModelState);
            }
            if (!_context.IsDistrictExists(Address.DistrictId))
            {
                ModelState.AddModelError("DistrictId", "Invalid DistrictId");
                return NotFound(ModelState);
            }
            if (!_context.IsWardExists(Address.WardId))
            {
                ModelState.AddModelError("WardId", "Invalid WardId");
                return NotFound(ModelState);
            }

            var NewAddress = _mapper.Map<Address>(Address);
            NewAddress.AddressId = AddressId;

            _context.Addresses.Update(NewAddress);

            await _context.SaveChangesAsync();

            return NewAddress;
        }

        [HttpDelete]
        public async Task<ActionResult<Address>> DeleteAddress(int AddressId)
        {
            var Address = await _context.Addresses.FindAsync(AddressId);
            if (Address is null)
            {
                ModelState.AddModelError("AddressId", "Invalid AddressId");
                return NotFound();
            }

            _context.Addresses.Remove(Address);
            await _context.SaveChangesAsync();

            return Address;
        }

        [HttpGet]
        [Route("{AddressId}")]
        public async Task<ActionResult<AddressDTO>> GetAddressById(int AddressId)
        {
            var Address = await _context.Addresses.FindAsync(AddressId);
            if (Address is null)
            {
                ModelState.AddModelError("AddressId", "Invalid AddressId");
                return NotFound(ModelState);
            }
            return _mapper.Map<AddressDTO>(Address);
        }

        [HttpGet]
        [Route("FilterByAddressName")]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> FilterByAddressName(string AddressName)
        {
            var Addresss = await _context.Addresses
                .Where(Address => Address.AddressText.Contains(AddressName))
                .Select(Address => _mapper.Map<AddressDTO>(Address))
                .ToListAsync();

            return Addresss;
        }
    }
}
