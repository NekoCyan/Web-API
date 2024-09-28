using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai1.Simple
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetListAsync();
        Task<IEnumerable<Account>> SearchAsync(Expression<Func<Account, bool>> expression, bool exportDTO = true);
        Task<Account> GetAsync(int id, bool exportDTO = true);
        Task<Account> CreateAsync(Account entity);
        Task<Account> UpdateAsync(Account entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public AccountService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Account>> GetListAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<IEnumerable<Account>> SearchAsync(Expression<Func<Account, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Accounts;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Customers)
                .Include(x => x.Employees)
                .Include(x => x.RoleUsers)
                .Select(x => new Account
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    Phone = x.Phone,
                    ImageId = x.ImageId,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Customers = x.Customers.Select(y => new Customer
                    {
                        Id = y.Id,
                        Code = y.Code,
                        CompanyName = y.CompanyName,
                        ContactName = y.ContactName,
                        ContactTitle = y.ContactTitle,
                        Phone = y.Phone,
                        AddressId = y.AddressId,
                        AccountId = y.AccountId,
                        Status = y.Status
                    }).ToList(),
                    Employees = x.Employees.Select(y => new Employee
                    {
                        Id = y.Id,
                        LastName = y.LastName,
                        FirstName = y.FirstName,
                        Title = y.Title,
                        BirthDate = y.BirthDate,
                        HireDate = y.HireDate,
                        Phone = y.Phone,
                        Photo = y.Photo,
                        PhotoPath = y.PhotoPath,
                        AddressId = y.AddressId,
                        AccountId = y.AccountId,
                        Status = y.Status
                    }).ToList(),
                    RoleUsers = x.RoleUsers.Select(y => new RoleUser
                    {
                        Id = y.Id,
                        RoleId = y.RoleId,
                        AccountId = y.AccountId,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status
                    }).ToList(),
                })
                .ToListAsync();
        }

        public async Task<Account> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Accounts;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.Customers)
                .Include(x => x.Employees)
                .Include(x => x.RoleUsers)
                .Select(x => new Account
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    Phone = x.Phone,
                    ImageId = x.ImageId,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    Customers = x.Customers.Select(y => new Customer
                    {
                        Id = y.Id,
                        Code = y.Code,
                        CompanyName = y.CompanyName,
                        ContactName = y.ContactName,
                        ContactTitle = y.ContactTitle,
                        Phone = y.Phone,
                        AddressId = y.AddressId,
                        AccountId = y.AccountId,
                        Status = y.Status
                    }).ToList(),
                    Employees = x.Employees.Select(y => new Employee
                    {
                        Id = y.Id,
                        LastName = y.LastName,
                        FirstName = y.FirstName,
                        Title = y.Title,
                        BirthDate = y.BirthDate,
                        HireDate = y.HireDate,
                        Phone = y.Phone,
                        Photo = y.Photo,
                        PhotoPath = y.PhotoPath,
                        AddressId = y.AddressId,
                        AccountId = y.AccountId,
                        Status = y.Status
                    }).ToList(),
                    RoleUsers = x.RoleUsers.Select(y => new RoleUser
                    {
                        Id = y.Id,
                        RoleId = y.RoleId,
                        AccountId = y.AccountId,
                        CreatedAt = y.CreatedAt,
                        CreatedBy = y.CreatedBy,
                        UpdatedAt = y.UpdatedAt,
                        UpdatedBy = y.UpdatedBy,
                        Status = y.Status
                    }).ToList(),
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> CreateAsync(Account entity)
        {
            var account = _mapper.Map<Account>(entity);

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> UpdateAsync(Account entity)
        {
            var mappedEntity = _mapper.Map<Account>(entity);
            _context.Accounts.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account is null)
                return 0;

            _context.Accounts.Remove(account);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Accounts.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Accounts.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Accounts.Any(x => x.Id == id);
        }
    }
}