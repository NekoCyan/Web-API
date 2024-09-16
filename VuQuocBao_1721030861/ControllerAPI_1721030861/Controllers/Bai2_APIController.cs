using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Controllers
{
    [Route("api/[controller]/[action]")]
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

        #region Address
        [HttpGet]
        public async Task<ActionResult<AddressDTO>> GetAddress(int AddressId)
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
        public async Task<ActionResult<IEnumerable<AddressDTO>>> FilterByAddressName(string AddressName)
        {
            var Addresss = await _context.Addresses
                .Where(Address => Address.AddressText.Contains(AddressName))
                .Select(Address => _mapper.Map<AddressDTO>(Address))
                .ToListAsync();

            return Addresss;
        }
        #endregion

        #region Country
        [HttpGet]
        public async Task<ActionResult<CountryDTO>> GetCountry(int CountryId)
        {
            var Country = await _context.Countries.FindAsync(CountryId);
            if (Country is null)
            {
                ModelState.AddModelError("CountryId", "Invalid CountryId");
                return NotFound(ModelState);
            }
            return _mapper.Map<CountryDTO>(Country);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetAllCountries()
        {
            var Countries = await _context.Countries
                .Select(Country => _mapper.Map<CountryDTO>(Country))
                .ToListAsync();

            return Countries;
        }
        [HttpPost]
        public async Task<ActionResult<Country>> CreateNewCountry(CountryDTO Country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var NewCountry = _mapper.Map<Country>(Country);

            var newCountryId = _context.Countries.Max(x => x.CountryId) + 1;
            NewCountry.CountryId = newCountryId;

            _context.Countries.Add(NewCountry);

            await _context.SaveChangesAsync();

            return NewCountry;
        }
        [HttpPut]
        public async Task<ActionResult<Country>> EditCountry(int CountryId, CountryDTO Country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsCountryExists(CountryId))
            {
                ModelState.AddModelError("CountryId", "Invalid CountryId");
                return NotFound(ModelState);
            }

            var NewCountry = _mapper.Map<Country>(Country);
            NewCountry.CountryId = CountryId;

            _context.Countries.Update(NewCountry);

            await _context.SaveChangesAsync();

            return NewCountry;
        }
        [HttpDelete]
        public async Task<ActionResult<Country>> DeleteCountry(int CountryId)
        {
            var Country = await _context.Countries.FindAsync(CountryId);
            if (Country is null)
            {
                ModelState.AddModelError("CountryId", "Invalid CountryId");
                return NotFound();
            }

            _context.Countries.Remove(Country);
            await _context.SaveChangesAsync();

            return Country;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> FilterByCountryName(string CountryName)
        {
            var Countries = await _context.Countries
                .Where(Country => Country.CountryName.Contains(CountryName))
                .Select(Country => _mapper.Map<CountryDTO>(Country))
                .ToListAsync();

            return Countries;
        }
        #endregion

        #region District
        [HttpGet]
        public async Task<ActionResult<DistrictDTO>> GetDistrict(int DistrictId)
        {
            var District = await _context.Districts.FindAsync(DistrictId);
            if (District is null)
            {
                ModelState.AddModelError("DistrictId", "Invalid DistrictId");
                return NotFound(ModelState);
            }
            return _mapper.Map<DistrictDTO>(District);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDTO>>> GetAllDistricts()
        {
            var Districts = await _context.Districts
                .Select(District => _mapper.Map<DistrictDTO>(District))
                .ToListAsync();

            return Districts;
        }
        [HttpPost]
        public async Task<ActionResult<District>> CreateNewDistrict(DistrictDTO District)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsProvinceExists(District.ProvinceId))
            {
                ModelState.AddModelError("ProvinceId", "Invalid ProvinceId");
                return NotFound(ModelState);
            }
            var NewDistrict = _mapper.Map<District>(District);

            var newDistrictId = _context.Districts.Max(x => x.DistrictId) + 1;
            NewDistrict.DistrictId = newDistrictId;

            _context.Districts.Add(NewDistrict);

            await _context.SaveChangesAsync();

            return NewDistrict;
        }
        [HttpPut]
        public async Task<ActionResult<District>> EditDistrict(int DistrictId, DistrictDTO District)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsDistrictExists(DistrictId))
            {
                ModelState.AddModelError("DistrictId", "Invalid DistrictId");
                return NotFound(ModelState);
            }
            if (!_context.IsProvinceExists(District.ProvinceId))
            {
                ModelState.AddModelError("ProvinceId", "Invalid ProvinceId");
                return NotFound(ModelState);
            }

            var NewDistrict = _mapper.Map<District>(District);
            NewDistrict.DistrictId = DistrictId;

            _context.Districts.Update(NewDistrict);

            await _context.SaveChangesAsync();

            return NewDistrict;
        }
        [HttpDelete]
        public async Task<ActionResult<District>> DeleteDistrict(int DistrictId)
        {
            var District = await _context.Districts.FindAsync(DistrictId);
            if (District is null)
            {
                ModelState.AddModelError("DistrictId", "Invalid DistrictId");
                return NotFound();
            }

            _context.Districts.Remove(District);
            await _context.SaveChangesAsync();

            return District;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDTO>>> FilterByDistrictName(string DistrictName)
        {
            var Districts = await _context.Districts
                .Where(District => District.DistrictName.Contains(DistrictName))
                .Select(District => _mapper.Map<DistrictDTO>(District))
                .ToListAsync();

            return Districts;
        }
        #endregion

        #region Province
        [HttpGet]
        public async Task<ActionResult<ProvinceDTO>> GetProvince(int ProvinceId)
        {
            var Province = await _context.Provinces.FindAsync(ProvinceId);
            if (Province is null)
            {
                ModelState.AddModelError("ProvinceId", "Invalid ProvinceId");
                return NotFound(ModelState);
            }
            return _mapper.Map<ProvinceDTO>(Province);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> GetAllProvinces()
        {
            var Provinces = await _context.Provinces
                .Select(Province => _mapper.Map<ProvinceDTO>(Province))
                .ToListAsync();

            return Provinces;
        }
        [HttpPost]
        public async Task<ActionResult<Province>> CreateNewProvince(ProvinceDTO Province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsCountryExists(Province.CountryId))
            {
                ModelState.AddModelError("CountryId", "Invalid CountryId");
                return NotFound(ModelState);
            }
            var NewProvince = _mapper.Map<Province>(Province);

            var newProvinceId = _context.Provinces.Max(x => x.ProvinceId) + 1;
            NewProvince.ProvinceId = newProvinceId;

            _context.Provinces.Add(NewProvince);

            await _context.SaveChangesAsync();

            return NewProvince;
        }
        [HttpPut]
        public async Task<ActionResult<Province>> EditProvince(int ProvinceId, ProvinceDTO Province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsProvinceExists(ProvinceId))
            {
                ModelState.AddModelError("ProvinceId", "Invalid ProvinceId");
                return NotFound(ModelState);
            }
            if (!_context.IsCountryExists(Province.CountryId))
            {
                ModelState.AddModelError("CountryId", "Invalid CountryId");
                return NotFound(ModelState);
            }

            var NewProvince = _mapper.Map<Province>(Province);
            NewProvince.ProvinceId = ProvinceId;

            _context.Provinces.Update(NewProvince);

            await _context.SaveChangesAsync();

            return NewProvince;
        }
        [HttpDelete]
        public async Task<ActionResult<Province>> DeleteProvince(int ProvinceId)
        {
            var Province = await _context.Provinces.FindAsync(ProvinceId);
            if (Province is null)
            {
                ModelState.AddModelError("ProvinceId", "Invalid ProvinceId");
                return NotFound();
            }

            _context.Provinces.Remove(Province);
            await _context.SaveChangesAsync();

            return Province;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceDTO>>> FilterByProvinceName(string ProvinceName)
        {
            var Provinces = await _context.Provinces
                .Where(Province => Province.ProvinceName.Contains(ProvinceName))
                .Select(Province => _mapper.Map<ProvinceDTO>(Province))
                .ToListAsync();

            return Provinces;
        }
        #endregion

        #region Ward
        [HttpGet]
        public async Task<ActionResult<WardDTO>> GetWard(int WardId)
        {
            var Ward = await _context.Wards.FindAsync(WardId);
            if (Ward is null)
            {
                ModelState.AddModelError("WardId", "Invalid WardId");
                return NotFound(ModelState);
            }
            return _mapper.Map<WardDTO>(Ward);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardDTO>>> GetAllWards()
        {
            var Wards = await _context.Wards
                .Select(Ward => _mapper.Map<WardDTO>(Ward))
                .ToListAsync();

            return Wards;
        }
        [HttpPost]
        public async Task<ActionResult<Ward>> CreateNewWard(WardDTO Ward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsDistrictExists(Ward.DistrictId))
            {
                ModelState.AddModelError("DistrictId", "Invalid DistrictId");
                return NotFound(ModelState);
            }
            var NewWard = _mapper.Map<Ward>(Ward);

            var newWardId = _context.Wards.Max(x => x.WardId) + 1;
            NewWard.WardId = newWardId;

            _context.Wards.Add(NewWard);

            await _context.SaveChangesAsync();

            return NewWard;
        }
        [HttpPut]
        public async Task<ActionResult<Ward>> EditWard(int WardId, WardDTO Ward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.IsWardExists(WardId))
            {
                ModelState.AddModelError("WardId", "Invalid WardId");
                return NotFound(ModelState);
            }
            if (!_context.IsDistrictExists(Ward.DistrictId))
            {
                ModelState.AddModelError("DistrictId", "Invalid DistrictId");
                return NotFound(ModelState);
            }

            var NewWard = _mapper.Map<Ward>(Ward);
            NewWard.WardId = WardId;

            _context.Wards.Update(NewWard);

            await _context.SaveChangesAsync();

            return NewWard;
        }
        [HttpDelete]
        public async Task<ActionResult<Ward>> DeleteWard(int WardId)
        {
            var Ward = await _context.Wards.FindAsync(WardId);
            if (Ward is null)
            {
                ModelState.AddModelError("WardId", "Invalid WardId");
                return NotFound();
            }

            _context.Wards.Remove(Ward);
            await _context.SaveChangesAsync();

            return Ward;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardDTO>>> FilterByWardName(string WardName)
        {
            var Wards = await _context.Wards
                .Where(Ward => Ward.WardName.Contains(WardName))
                .Select(Ward => _mapper.Map<WardDTO>(Ward))
                .ToListAsync();

            return Wards;
        }
        #endregion
    }
}
