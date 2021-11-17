using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.Models
{
  public class TodoListModel
  {
    public int Id { get; set; }
    [Required]

    public string Name { get; set; }

    public List<Todo> Todos { get; set; }

    //add parent ref to Todo List
    public int UsersId { get; set; }
    public User Users { get; set; }

  }
}
