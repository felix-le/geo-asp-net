using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TodoList.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {

    // make global references from model to D
    public DbSet<TodoListContent> TodoLists { get; set; }
    public DbSet<Todo> Todos { get; set; }

    public DbSet<UserProfile> UserProfiles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
  }
}

