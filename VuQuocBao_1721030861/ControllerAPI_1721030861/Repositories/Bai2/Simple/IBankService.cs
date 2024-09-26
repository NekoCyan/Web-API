using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai2.Simple
{
    public interface IBankService
    {
        Task<IEnumerable<Bank>> GetListAsync();
        Task<IEnumerable<Bank>> SearchAsync(Expression<Func<Bank, bool>> expression, bool exportDTO = true);
        Task<Bank> GetAsync(int id, bool exportDTO = true);
        Task<Bank> CreateAsync(Bank entity);
        Task<Bank> UpdateAsync(Bank entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class BankService : IBankService
    {
        private readonly GeneralCatalogContext _context;
        private readonly IMapper _mapper;

        public BankService(GeneralCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Bank>> GetListAsync()
        {
            return await _context.Banks.ToListAsync();
        }

        public async Task<IEnumerable<Bank>> SearchAsync(Expression<Func<Bank, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Banks;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.BankType)
                .Select(x => new Bank
                {
                    Id = x.Id,
                    BankTypeId = x.BankTypeId,
                    Name = x.Name,
                    NameEn = x.NameEn,
                    TradeName = x.TradeName,
                    SiteUrl = x.SiteUrl,
                    Status = x.Status,
                    IsDefault = x.IsDefault,
                    Description = x.Description,
                    Timer = x.Timer,
                    BankType = new BankType
                    {
                        Id = x.BankType.Id,
                        Name = x.BankType.Name,
                        Status = x.BankType.Status,
                        CreatedAt = x.BankType.CreatedAt,
                        CreatedBy = x.BankType.CreatedBy,
                        UpdatedAt = x.BankType.UpdatedAt,
                        UpdatedBy = x.BankType.UpdatedBy,
                        Timer = x.BankType.Timer,
                    },
                })
                .ToListAsync();
        }

        public async Task<Bank> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Banks;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.BankType)
                .Select(x => new Bank
                {
                    Id = x.Id,
                    BankTypeId = x.BankTypeId,
                    Name = x.Name,
                    NameEn = x.NameEn,
                    TradeName = x.TradeName,
                    SiteUrl = x.SiteUrl,
                    Status = x.Status,
                    IsDefault = x.IsDefault,
                    Description = x.Description,
                    Timer = x.Timer,
                    BankType = new BankType
                    {
                        Id = x.BankType.Id,
                        Name = x.BankType.Name,
                        Status = x.BankType.Status,
                        CreatedAt = x.BankType.CreatedAt,
                        CreatedBy = x.BankType.CreatedBy,
                        UpdatedAt = x.BankType.UpdatedAt,
                        UpdatedBy = x.BankType.UpdatedBy,
                        Timer = x.BankType.Timer,
                    },
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Bank> CreateAsync(Bank entity)
        {
            var bank = _mapper.Map<Bank>(entity);
            bank.Timer = DateTime.Now;

            await _context.Banks.AddAsync(bank);
            await _context.SaveChangesAsync();

            return bank;
        }

        public async Task<Bank> UpdateAsync(Bank entity)
        {
            var mappedEntity = _mapper.Map<Bank>(entity);
            _context.Banks.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var bank = _context.Banks.Find(id);
            if (bank is null)
                return 0;

            _context.Banks.Remove(bank);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Banks.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Banks.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Banks.Any(x => x.Id == id);
        }
    }
}