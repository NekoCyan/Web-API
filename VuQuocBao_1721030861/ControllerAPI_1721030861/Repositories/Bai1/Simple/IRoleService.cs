using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Bai1.Simple
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetListAsync();
        Task<IEnumerable<Role>> SearchAsync(Expression<Func<Role, bool>> expression, bool exportDTO = true);
        Task<Role> GetAsync(int id, bool exportDTO = true);
        Task<Role> CreateAsync(Role entity);
        Task<Role> UpdateAsync(Role entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class RoleService : IRoleService
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public RoleService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Role>> GetListAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> SearchAsync(Expression<Func<Role, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.Roles;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.RoleUsers)
                .Select(x => new Role
                {
                    Id = x.Id,
                    Name = x.Name,
                    Notes = x.Notes,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
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
                }).ToListAsync();
        }

        public async Task<Role> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.Roles;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.RoleUsers)
                .Select(x => new Role
                {
                    Id = x.Id,
                    Name = x.Name,
                    Notes = x.Notes,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
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
                }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Role> CreateAsync(Role entity)
        {
            var role = _mapper.Map<Role>(entity);

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role> UpdateAsync(Role entity)
        {
            var mappedEntity = _mapper.Map<Role>(entity);
            _context.Roles.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role is null)
                return 0;

            _context.Roles.Remove(role);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.Roles.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.Roles.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.Roles.Any(x => x.Id == id);
        }
    }
}