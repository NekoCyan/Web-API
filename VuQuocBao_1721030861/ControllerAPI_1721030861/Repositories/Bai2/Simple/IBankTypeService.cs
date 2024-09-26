using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai2.Simple
{
    public interface IBankTypeService
    {
        Task<IEnumerable<BankType>> GetListAsync();
        Task<IEnumerable<BankType>> SearchAsync(Expression<Func<BankType, bool>> expression, bool exportDTO = true);
        Task<BankType> GetAsync(int id, bool exportDTO = true);
        Task<BankType> CreateAsync(BankType entity);
        Task<BankType> UpdateAsync(BankType entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class BankTypeService : IBankTypeService
    {
        private readonly GeneralCatalogContext _context;
        private readonly IMapper _mapper;

        public BankTypeService(GeneralCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankType>> GetListAsync()
        {
            return await _context.BankTypes.ToListAsync();
        }

        public async Task<IEnumerable<BankType>> SearchAsync(Expression<Func<BankType, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.BankTypes;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Banks)
                .Select(x => new BankType
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Timer = x.Timer,
                    Banks = x.Banks.Select(y => new Bank
                    {
                        Id = y.Id,
                        BankTypeId = y.BankTypeId,
                        Name = y.Name,
                        NameEn = y.NameEn,
                        TradeName = y.TradeName,
                        SiteUrl = y.SiteUrl,
                        Status = y.Status,
                        IsDefault = y.IsDefault,
                        Description = y.Description,
                        Timer = y.Timer,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<BankType> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.BankTypes;
            if (exportDTO)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.Banks)
                .Select(x => new BankType
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Timer = x.Timer,
                    Banks = x.Banks.Select(y => new Bank
                    {
                        Id = y.Id,
                        BankTypeId = y.BankTypeId,
                        Name = y.Name,
                        NameEn = y.NameEn,
                        TradeName = y.TradeName,
                        SiteUrl = y.SiteUrl,
                        Status = y.Status,
                        IsDefault = y.IsDefault,
                        Description = y.Description,
                        Timer = y.Timer,
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BankType> CreateAsync(BankType entity)
        {
            var mappedEntity = _mapper.Map<BankType>(entity);
            _context.BankTypes.Add(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public async Task<BankType> UpdateAsync(BankType entity)
        {
            var mappedEntity = _mapper.Map<BankType>(entity);
            _context.BankTypes.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var entity = _context.BankTypes.Find(id);
            if (entity == null)
                return 0;

            _context.BankTypes.Remove(entity);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.BankTypes.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.BankTypes.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.BankTypes.Any(x => x.Id == id);
        }
    }
}
