using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai1.Simple
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetListAsync();
        Task<IEnumerable<Employee>> SearchAsync(Expression<Func<Employee, bool>> expression, bool exportDTO = true);
        Task<Employee> GetAsync(int id, bool exportDTO = true);
        Task<Employee> CreateAsync(Employee entity);
        Task<Employee> UpdateAsync(Employee entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> GetListAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchAsync(Expression<Func<Employee, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Employees;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Account)
                .Include(x => x.Orders)
                .Select(x => new Employee
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    BirthDate = x.BirthDate,
                    HireDate = x.HireDate,
                    Phone = x.Phone,
                    Photo = x.Photo,
                    PhotoPath = x.PhotoPath,
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

        public async Task<Employee> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Employees;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.Account)
                .Include(x => x.Orders)
                .Select(x => new Employee
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    BirthDate = x.BirthDate,
                    HireDate = x.HireDate,
                    Phone = x.Phone,
                    Photo = x.Photo,
                    PhotoPath = x.PhotoPath,
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

        public async Task<Employee> CreateAsync(Employee entity)
        {
            var employee = _mapper.Map<Employee>(entity);

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee entity)
        {
            var mappedEntity = _mapper.Map<Employee>(entity);
            _context.Employees.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee is null)
                return 0;

            _context.Employees.Remove(employee);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Employees.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Employees.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Employees.Any(x => x.Id == id);
        }
    }
}