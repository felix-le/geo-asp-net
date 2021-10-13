using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.Models
{
  public class Todo
  {
    public int Id { get; set; }

    public DateTime Deadline { get; set; }

    [Required]
    [Range(0, 99)]
    public int DaysTime { get; set; }

    [Required]
    [Display(Name = "Todo Name")]
    public string TodoName { get; set; }
    [Required]
    public int TodoListModelId { get; set; }

    //add parent ref to Todo List
    public TodoListModel TodoList { get; set; }

  }
}
