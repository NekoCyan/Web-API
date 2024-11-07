using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

#pragma warning disable CS8603 // Possible null reference return.

namespace ControllerAPI_1721030861.Repositories.First_Approach
{
    public class UserService : IRepository<UserApi>
    {
        private readonly FinalExamApiContext _context;
        private readonly IMapper _mapper;

        public UserService(FinalExamApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserApi>> GetListAsync()
        {
            return await _context.UserApis.ToListAsync();
        }

        public async Task<IEnumerable<UserApi>> SearchAsync(Expression<Func<UserApi, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.UserApis;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.NewsApis)
                .Select(x => new UserApi
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    Phone = x.Phone,
                    NewsApis = x.NewsApis.Select(y => new NewsApi
                    {
                        Id = y.Id,
                        CategoryId = y.CategoryId,
                        Title = y.Title,
                        Description = y.Description,
                        Detail = y.Detail,
                        ImageFile = y.ImageFile,
                        PublishDate = y.PublishDate,
                        UserId = y.UserId,
                    }).ToList(),
                })
                .ToListAsync();
        }

        public async Task<UserApi> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.UserApis;

            if (exportDTO is true)
                return await ctx.FindAsync(id);

            return await ctx
                .Include(x => x.NewsApis)
                .Select(x => new UserApi
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    Phone = x.Phone,
                    NewsApis = x.NewsApis.Select(y => new NewsApi
                    {
                        Id = y.Id,
                        CategoryId = y.CategoryId,
                        Title = y.Title,
                        Description = y.Description,
                        Detail = y.Detail,
                        ImageFile = y.ImageFile,
                        PublishDate = y.PublishDate,
                        UserId = y.UserId,
                    }).ToList(),
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserApi> CreateAsync(UserApi entity)
        {
            var user = _mapper.Map<UserApi>(entity);
            await _context.UserApis.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserApi> UpdateAsync(UserApi entity)
        {
            var mappedEntity = _mapper.Map<UserApi>(entity);
            _context.UserApis.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var user = _context.UserApis.Find(id);
            if (user is null)
                return 0;

            _context.UserApis.Remove(user);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.UserApis.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.UserApis.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.UserApis.Any(x => x.Id == id);
        }
    }
}
