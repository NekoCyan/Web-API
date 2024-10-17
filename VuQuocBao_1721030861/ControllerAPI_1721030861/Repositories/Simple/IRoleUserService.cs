using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControllerAPI_1721030861.Repositories.Simple
{
    public interface IRoleUserService
    {
        Task<IEnumerable<RoleUser>> GetListAsync();
        Task<IEnumerable<RoleUser>> SearchAsync(Expression<Func<RoleUser, bool>> expression, bool exportDTO = true);
        Task<RoleUser> GetAsync(int id, bool exportDTO = true);
        Task<RoleUser> CreateAsync(RoleUser entity);
        Task<RoleUser> UpdateAsync(RoleUser entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class RoleUserService : IRoleUserService
    {
        private readonly APITeachingContext _context;
        private readonly IMapper _mapper;

        public RoleUserService(APITeachingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleUser>> GetListAsync()
        {
            return await _context.RoleUsers.ToListAsync();
        }

        public async Task<IEnumerable<RoleUser>> SearchAsync(Expression<Func<RoleUser, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.RoleUsers;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.Account)
                .Include(x => x.Role)
                .Select(x => new RoleUser
                {
                    Id = x.Id,
                    RoleId = x.RoleId,
                    AccountId = x.AccountId,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
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
                    Role = new Role
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name,
                        Notes = x.Role.Notes,
                        CreatedAt = x.Role.CreatedAt,
                        CreatedBy = x.Role.CreatedBy,
                        UpdatedAt = x.Role.UpdatedAt,
                        UpdatedBy = x.Role.UpdatedBy,
                        Status = x.Role.Status
                    }
                })
                .ToListAsync();
        }

        public async Task<RoleUser> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.RoleUsers;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.Account)
                .Include(x => x.Role)
                .Select(x => new RoleUser
                {
                    Id = x.Id,
                    RoleId = x.RoleId,
                    AccountId = x.AccountId,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
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
                    Role = new Role
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name,
                        Notes = x.Role.Notes,
                        CreatedAt = x.Role.CreatedAt,
                        CreatedBy = x.Role.CreatedBy,
                        UpdatedAt = x.Role.UpdatedAt,
                        UpdatedBy = x.Role.UpdatedBy,
                        Status = x.Role.Status
                    }
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RoleUser> CreateAsync(RoleUser entity)
        {
            var roleUser = _mapper.Map<RoleUser>(entity);

            await _context.RoleUsers.AddAsync(roleUser);
            await _context.SaveChangesAsync();

            return roleUser;
        }

        public async Task<RoleUser> UpdateAsync(RoleUser entity)
        {
            var mappedEntity = _mapper.Map<RoleUser>(entity);
            _context.RoleUsers.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var roleUser = _context.RoleUsers.Find(id);
            if (roleUser is null)
                return 0;

            _context.RoleUsers.Remove(roleUser);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.RoleUsers.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.RoleUsers.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.RoleUsers.Any(x => x.Id == id);
        }
    }
}