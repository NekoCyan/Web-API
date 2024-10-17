using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.First_Approach
{
    public class WardService : IRepository<Ward>
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public WardService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Ward>> GetListAsync()
        {
            return await _context.Wards.ToListAsync();
        }

        public async Task<IEnumerable<Ward>> SearchAsync(Expression<Func<Ward, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Wards;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Addresses)
                .Select(x => new Ward
                {
                    Id = x.Id,
                    Name = x.Name,
                    WardCode = x.WardCode,
                    DistrictId = x.DistrictId,
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

        public async Task<Ward> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Wards;

            if (exportDTO is true)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.Addresses)
                .Select(x => new Ward
                {
                    Id = x.Id,
                    Name = x.Name,
                    WardCode = x.WardCode,
                    DistrictId = x.DistrictId,
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

        public async Task<Ward> CreateAsync(Ward entity)
        {
            var ward = _mapper.Map<Ward>(entity);
            await _context.Wards.AddAsync(ward);
            await _context.SaveChangesAsync();
            return ward;
        }

        public async Task<Ward> UpdateAsync(Ward entity)
        {
            var mappedEntity = _mapper.Map<Ward>(entity);
            _context.Wards.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var ward = _context.Wards.Find(id);
            if (ward is null)
                return 0;

            _context.Wards.Remove(ward);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Wards.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Wards.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Wards.Any(x => x.Id == id);
        }
    }
}
