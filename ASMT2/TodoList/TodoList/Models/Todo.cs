using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
  public class Todo
  {
    public int TodoId { get; set; }

    [Required]
    public string TodoName { get; set; }

    [Required]
    public DateTime Deadline { get; set; }

    [Required]
    public bool IsFinished { get; set; }
  }
}
