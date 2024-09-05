using Microsoft.EntityFrameworkCore;
using MinimalAPI_1721030861.Database.Models;

namespace MinimalAPI_1721030861.Database
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
