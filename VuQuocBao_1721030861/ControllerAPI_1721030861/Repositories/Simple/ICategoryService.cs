using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

#pragma warning disable CS8603 // Possible null reference return.

namespace ControllerAPI_1721030861.Repositories.Simple
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryApi>> GetListAsync();
        Task<IEnumerable<CategoryApi>> SearchAsync(Expression<Func<CategoryApi, bool>> expression, bool exportDTO = true);
        Task<CategoryApi> GetAsync(int id, bool exportDTO = true);
        Task<CategoryApi> CreateAsync(CategoryApi entity);
        Task<CategoryApi> UpdateAsync(CategoryApi entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly FinalExamApiContext _context;
        private readonly IMapper _mapper;

        public CategoryService(FinalExamApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryApi>> GetListAsync()
        {
            return await _context.CategoryApis.ToListAsync();
        }

        public async Task<IEnumerable<CategoryApi>> SearchAsync(Expression<Func<CategoryApi, bool>> expression, bool exportDTO = true)
        {
            var ctx = _context.CategoryApis;

            if (exportDTO is true)
                return await ctx.Where(expression).ToListAsync();

            return await ctx
                .Where(expression)
                .Include(x => x.NewsApis)
                .Select(x => new CategoryApi
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
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

        public async Task<CategoryApi> GetAsync(int id, bool exportDTO = true)
        {
            var ctx = _context.CategoryApis;

            if (exportDTO is true)
                return await ctx.FirstOrDefaultAsync(x => x.Id == id);

            return await ctx
                .Include(x => x.NewsApis)
                .Select(x => new CategoryApi
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
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

        public async Task<CategoryApi> CreateAsync(CategoryApi entity)
        {
            var category = _mapper.Map<CategoryApi>(entity);

            await _context.CategoryApis.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<CategoryApi> UpdateAsync(CategoryApi entity)
        {
            var mappedEntity = _mapper.Map<CategoryApi>(entity);
            _context.CategoryApis.Update(mappedEntity);
            await _context.SaveChangesAsync();
            return mappedEntity;
        }

        public int Delete(int id)
        {
            var category = _context.CategoryApis.Find(id);
            if (category is null)
                return 0;

            _context.CategoryApis.Remove(category);
            return _context.SaveChanges();
        }

        public async Task<int> MaxIdAsync(int id)
        {
            return await _context.CategoryApis.MaxAsync(x => x.Id);
        }

        public async Task<int> MinIdAsync(int id)
        {
            return await _context.CategoryApis.MinAsync(x => x.Id);
        }

        public bool CheckExists(int id)
        {
            return _context.CategoryApis.Any(x => x.Id == id);
        }
    }
}