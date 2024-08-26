using Microsoft.EntityFrameworkCore;
using Minimal.Database.Models;

namespace Minimal.Database
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
