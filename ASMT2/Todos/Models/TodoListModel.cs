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
  }
}
