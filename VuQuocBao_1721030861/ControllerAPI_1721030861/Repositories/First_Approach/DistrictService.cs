using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.First_Approach
{
    public class DistrictService : IRepository<District>
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public DistrictService(APITeachingContext context, IMapper mapper)
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
                .Include(x => x.Addresses)
                .Select(x => new District
                {
                    Id = x.Id,
                    Name = x.Name,
                    DistrictCode = x.DistrictCode,
                    ProvinceId = x.ProvinceId,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Addresses = x.Addresses.Select(y => new Address
                    {
                        Id = y.Id,
                        AddressText = y.AddressText,
                        CountryId = y.CountryId,
                        ProvinceId = y.ProvinceId,
                        DistrictId = y.DistrictId,
                        WardId = y.WardId,
                        TownId = y.TownId,
                        Latitude = y.Latitude,
                        Longitude = y.Longitude,
                        Notes = y.Notes,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<District> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Districts;

            if (exportDTO is true)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.Addresses)
                .Select(x => new District
                {
                    Id = x.Id,
                    Name = x.Name,
                    DistrictCode = x.DistrictCode,
                    ProvinceId = x.ProvinceId,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Addresses = x.Addresses.Select(y => new Address
                    {
                        Id = y.Id,
                        AddressText = y.AddressText,
                        CountryId = y.CountryId,
                        ProvinceId = y.ProvinceId,
                        DistrictId = y.DistrictId,
                        WardId = y.WardId,
                        TownId = y.TownId,
                        Latitude = y.Latitude,
                        Longitude = y.Longitude,
                        Notes = y.Notes,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status,
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<District> CreateAsync(District entity)
        {
            var district = _mapper.Map<District>(entity);
            await _context.Districts.AddAsync(district);
            await _context.SaveChangesAsync();
            return district;
        }

        public async Task<District> UpdateAsync(District entity)
        {
            var mappedEntity = _mapper.Map<District>(entity);
            _context.Districts.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var district = _context.Districts.Find(id);
            if (district is null)
                return 0;

            _context.Districts.Remove(district);
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
