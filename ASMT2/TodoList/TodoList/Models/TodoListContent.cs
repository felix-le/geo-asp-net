using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
  public class TodoList
  {
    public int TodoListId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int TodoId { get; set; }

    // add parent ref to Todo List (1 user => Many todo List)
    public User User { get; set; }

    //ref to child model ( 1 todo list has many to do)
    public List<Todo> Todos { get; set; }

    public int Total { get; set; }
  }
}
