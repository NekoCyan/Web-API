using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai2.First_Approach
{
    public class DistrictService : IRepository<District>
    {
        private readonly GeneralCatalogContext _context;
        private readonly IMapper _mapper;

        public DistrictService(GeneralCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<District>> GetListAsync()
        {
            return await _context.Districts.ToListAsync();
        }

        public async Task<IEnumerable<District>> SearchAsync(Expression<Func<District, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Districts;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Province)
                .Include(x => x.Wards)
                .Select(x => new District
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameSlug = x.NameSlug,
                    DistrictCode = x.DistrictCode,
                    ProvinceId = x.ProvinceId,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Timer = x.Timer,
                    Province = new Province
                    {
                        Id = x.Province.Id,
                        Name = x.Province.Name,
                        NameSlug = x.Province.NameSlug,
                        ProvinceCode = x.Province.ProvinceCode,
                        AxisMeridian = x.Province.AxisMeridian,
                        CreatedAt = x.Province.CreatedAt,
                        CreatedBy = x.Province.CreatedBy,
                        UpdatedAt = x.Province.UpdatedAt,
                        UpdatedBy = x.Province.UpdatedBy,
                        Status = x.Province.Status,
                        Timer = x.Province.Timer,
                        CountryId = x.Province.CountryId
                    },
                    Wards = x.Wards.Select(y => new Ward
                    {
                        Id = y.Id,
                        Name = y.Name,
                        NameSlug = y.NameSlug,
                        WardCode = y.WardCode,
                        DistrictId = y.DistrictId,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                        Timer = y.Timer
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<District> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Districts;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.Province)
                .Include(x => x.Wards)
                .Select(x => new District
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameSlug = x.NameSlug,
                    DistrictCode = x.DistrictCode,
                    ProvinceId = x.ProvinceId,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Timer = x.Timer,
                    Province = new Province
                    {
                        Id = x.Province.Id,
                        Name = x.Province.Name,
                        NameSlug = x.Province.NameSlug,
                        ProvinceCode = x.Province.ProvinceCode,
                        AxisMeridian = x.Province.AxisMeridian,
                        CreatedAt = x.Province.CreatedAt,
                        CreatedBy = x.Province.CreatedBy,
                        UpdatedAt = x.Province.UpdatedAt,
                        UpdatedBy = x.Province.UpdatedBy,
                        Status = x.Province.Status,
                        Timer = x.Province.Timer,
                        CountryId = x.Province.CountryId
                    },
                    Wards = x.Wards.Select(y => new Ward
                    {
                        Id = y.Id,
                        Name = y.Name,
                        NameSlug = y.NameSlug,
                        WardCode = y.WardCode,
                        DistrictId = y.DistrictId,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                        Timer = y.Timer
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<District> CreateAsync(District entity)
        {
            var model = _mapper.Map<District>(entity);
            await _context.Districts.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<District> UpdateAsync(District entity)
        {
            var model = _mapper.Map<District>(entity);
            _context.Districts.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public int Delete(int id)
        {
            var model = _context.Districts.Find(id);
            if (model is null)
                return 0;

            _context.Districts.Remove(model);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Districts.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Districts.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Districts.Any(x => x.Id == id);
        }
    }
}
