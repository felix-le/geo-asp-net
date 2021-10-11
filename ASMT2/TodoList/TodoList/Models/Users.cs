using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
  public class Users
  {
    public int UserId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

  }
}
