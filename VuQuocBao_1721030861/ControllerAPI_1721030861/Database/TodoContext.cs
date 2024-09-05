using ControllerAPI_1721030861.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Database
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> TodoItems { get; set; } = null!;
    }
}
