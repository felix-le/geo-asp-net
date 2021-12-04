using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Todos.Models;
using Microsoft.EntityFrameworkCore;

namespace Todos.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {

    // make global references from model to D
    public DbSet<TodoListModel> TodoListModels { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Todos.Models.User> User { get; set; }
  }
}

