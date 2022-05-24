using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDTO.DAL
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
