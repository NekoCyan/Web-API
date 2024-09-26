using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai2.First_Approach
{
    public class ProvinceService : IRepository<Province>
    {
        private readonly GeneralCatalogContext _context;
        private readonly IMapper _mapper;

        public ProvinceService(GeneralCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Province>> GetListAsync()
        {
            return await _context.Provinces.ToListAsync();
        }

        public async Task<IEnumerable<Province>> SearchAsync(Expression<Func<Province, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Provinces;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Country)
                .Include(x => x.Districts)
                .Select(x => new Province
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameSlug = x.NameSlug,
                    ProvinceCode = x.ProvinceCode,
                    AxisMeridian = x.AxisMeridian,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Timer = x.Timer,
                    Country = new Country
                    {
                        Id = x.Country.Id,
                        Name = x.Country.Name,
                        NameSlug = x.Country.NameSlug,
                        CountryCode = x.Country.CountryCode,
                        Status = x.Country.Status,
                        CreatedAt = x.Country.CreatedAt,
                        CreatedBy = x.Country.CreatedBy,
                        UpdatedAt = x.Country.UpdatedAt,
                        UpdatedBy = x.Country.UpdatedBy,
                        Remark = x.Country.Remark,
                        Timer = x.Country.Timer,
                    },
                    Districts = x.Districts.Select(y => new District
                    {
                        Id = y.Id,
                        Name = y.Name,
                        NameSlug = y.NameSlug,
                        DistrictCode = y.DistrictCode,
                        ProvinceId = y.ProvinceId,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                        Timer = y.Timer,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<Province> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Provinces;

            if (exportDTO is true)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.Country)
                .Include(x => x.Districts)
                .Select(x => new Province
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameSlug = x.NameSlug,
                    ProvinceCode = x.ProvinceCode,
                    AxisMeridian = x.AxisMeridian,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Timer = x.Timer,
                    Country = new Country
                    {
                        Id = x.Country.Id,
                        Name = x.Country.Name,
                        NameSlug = x.Country.NameSlug,
                        CountryCode = x.Country.CountryCode,
                        Status = x.Country.Status,
                        CreatedAt = x.Country.CreatedAt,
                        CreatedBy = x.Country.CreatedBy,
                        UpdatedAt = x.Country.UpdatedAt,
                        UpdatedBy = x.Country.UpdatedBy,
                        Remark = x.Country.Remark,
                        Timer = x.Country.Timer,
                    },
                    Districts = x.Districts.Select(y => new District
                    {
                        Id = y.Id,
                        Name = y.Name,
                        NameSlug = y.NameSlug,
                        DistrictCode = y.DistrictCode,
                        ProvinceId = y.ProvinceId,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                        Timer = y.Timer,
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Province> CreateAsync(Province entity)
        {
            var province = _mapper.Map<Province>(entity);
            await _context.Provinces.AddAsync(province);
            await _context.SaveChangesAsync();
            return province;
        }

        public async Task<Province> UpdateAsync(Province entity)
        {
            var mappedEntity = _mapper.Map<Province>(entity);
            _context.Provinces.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var country = _context.Provinces.Find(id);
            if (country is null)
                return 0;

            _context.Provinces.Remove(country);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Provinces.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Provinces.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Provinces.Any(x => x.Id == id);
        }
    }
}
