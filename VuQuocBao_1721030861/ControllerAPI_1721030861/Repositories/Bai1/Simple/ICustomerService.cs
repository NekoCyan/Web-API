using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai1.Simple
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetListAsync();
        Task<IEnumerable<Customer>> SearchAsync(Expression<Func<Customer, bool>> expression, bool exportDTO = true);
        Task<Customer> GetAsync(int id, bool exportDTO = true);
        Task<Customer> CreateAsync(Customer entity);
        Task<Customer> UpdateAsync(Customer entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public CustomerService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer>> GetListAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> SearchAsync(Expression<Func<Customer, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Customers;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Account)
                .Include(x => x.Orders)
                .Select(x => new Customer
                {
                    Id = x.Id,
                    Code = x.Code,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Phone = x.Phone,
                    AddressId = x.AddressId,
                    AccountId = x.AccountId,
                    Status = x.Status,
                    Account = new Account
                    {
                        Id = x.Account.Id,
                        UserName = x.Account.UserName,
                        Password = x.Account.Password,
                        Email = x.Account.Email,
                        Phone = x.Account.Phone,
                        ImageId = x.Account.ImageId,
                        CreatedAt = x.Account.CreatedAt,
                        CreatedBy = x.Account.CreatedBy,
                        UpdatedAt = x.Account.UpdatedAt,
                        UpdatedBy = x.Account.UpdatedBy,
                        Status = x.Account.Status
                    },
                    Orders = x.Orders.Select(y => new Order
                    {
                        Id = y.Id,
                        CustomerId = y.CustomerId,
                        EmployeeId = y.EmployeeId,
                        OrderDate = y.OrderDate,
                        RequiredDate = y.RequiredDate,
                        ShippedDate = y.ShippedDate,
                        ShipId = y.ShipId,
                        Freight = y.Freight,
                        ShipAddress = y.ShipAddress,
                        Status = y.Status,
                    }).ToList(),
                }).ToListAsync();
        }

        public async Task<Customer> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Customers;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.Account)
                .Include(x => x.Orders)
                .Select(x => new Customer
                {
                    Id = x.Id,
                    Code = x.Code,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Phone = x.Phone,
                    AddressId = x.AddressId,
                    AccountId = x.AccountId,
                    Status = x.Status,
                    Account = new Account
                    {
                        Id = x.Account.Id,
                        UserName = x.Account.UserName,
                        Password = x.Account.Password,
                        Email = x.Account.Email,
                        Phone = x.Account.Phone,
                        ImageId = x.Account.ImageId,
                        CreatedAt = x.Account.CreatedAt,
                        CreatedBy = x.Account.CreatedBy,
                        UpdatedAt = x.Account.UpdatedAt,
                        UpdatedBy = x.Account.UpdatedBy,
                        Status = x.Account.Status
                    },
                    Orders = x.Orders.Select(y => new Order
                    {
                        Id = y.Id,
                        CustomerId = y.CustomerId,
                        EmployeeId = y.EmployeeId,
                        OrderDate = y.OrderDate,
                        RequiredDate = y.RequiredDate,
                        ShippedDate = y.ShippedDate,
                        ShipId = y.ShipId,
                        Freight = y.Freight,
                        ShipAddress = y.ShipAddress,
                        Status = y.Status,
                    }).ToList(),
                }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> CreateAsync(Customer entity)
        {
            var customer = _mapper.Map<Customer>(entity);

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            var mappedEntity = _mapper.Map<Customer>(entity);
            _context.Customers.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer is null)
                return 0;

            _context.Customers.Remove(customer);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Customers.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Customers.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Customers.Any(x => x.Id == id);
        }
    }
}