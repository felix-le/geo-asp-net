using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Thusday_lession1.Models;

namespace Thusday_lession1.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {

    //Make global references to our Models for use with our DB connection
    // These 5 objects will have built-in CRUD methods we can execute without using SQL

    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }
  }
}
