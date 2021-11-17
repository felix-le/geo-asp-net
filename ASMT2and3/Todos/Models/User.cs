using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.Models
{
  public class User
  {
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    //ref child to Todo List
    public List<TodoListModel> TodoListModels { get; set; }
  }
}
