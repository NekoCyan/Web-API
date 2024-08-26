using ControllerAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI.Database
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoModel> TodoItems { get; set; } = null!;
    }
}
