using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
  public class TodoListContent
  {
    public int TodoListContentId { get; set; }

    [Required]
    public int UserProfile { get; set; }

    [Required]
    public int TodoId { get; set; }

    // add parent ref to Todo List (1 user => Many todo List)
    public UserProfile User { get; set; }

    //ref to child model ( 1 todo list has many to do)
    public List<Todo> Todos { get; set; }

    public int Total { get; set; }
  }
}
