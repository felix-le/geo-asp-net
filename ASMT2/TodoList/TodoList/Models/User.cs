using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
  public class User
  {
    public int UserId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public int ListId { get; set; }

    //ref to child model ( 1 user has many to do list)
    public List<TodoList> Lists { get; set; }
  }
}
