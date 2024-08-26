using Microsoft.EntityFrameworkCore;
using Minimal_API.Database.Models;

namespace Minimal_API.Database
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
