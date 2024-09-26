using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai2.First_Approach
{
    public class CountryService : IRepository<Country>
    {
        private readonly GeneralCatalogContext _context;
        private readonly IMapper _mapper;

        public CountryService(GeneralCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Country>> GetListAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<IEnumerable<Country>> SearchAsync(Expression<Func<Country, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Countries;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Provinces)
                .Select(x => new Country
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameSlug = x.NameSlug,
                    CountryCode = x.CountryCode,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Remark = x.Remark,
                    Timer = x.Timer,
                    Provinces = x.Provinces.Select(y => new Province
                    {
                        Id = y.Id,
                        Name = y.Name,
                        NameSlug = y.NameSlug,
                        ProvinceCode = y.ProvinceCode,
                        AxisMeridian = y.AxisMeridian,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                        Timer = y.Timer,
                        CountryId = y.CountryId
                    }).ToList(),
                })
                .ToListAsync();
        }

        public async Task<Country> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Countries;

            if (exportDTO is true)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.Provinces)
                .Select(x => new Country
                {
                    Id = x.Id,
                    Name = x.Name,
                    NameSlug = x.NameSlug,
                    CountryCode = x.CountryCode,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Remark = x.Remark,
                    Timer = x.Timer,
                    Provinces = x.Provinces.Select(y => new Province
                    {
                        Id = y.Id,
                        Name = y.Name,
                        NameSlug = y.NameSlug,
                        ProvinceCode = y.ProvinceCode,
                        AxisMeridian = y.AxisMeridian,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                        Timer = y.Timer,
                        CountryId = y.CountryId
                    }).ToList(),
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Country> CreateAsync(Country entity)
        {
            var country = _mapper.Map<Country>(entity);
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<Country> UpdateAsync(Country entity)
        {
            var mappedEntity = _mapper.Map<Country>(entity);
            _context.Countries.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var country = _context.Countries.Find(id);
            if (country is null)
                return 0;

            _context.Countries.Remove(country);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Countries.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Countries.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Countries.Any(x => x.Id == id);
        }
    }
}
