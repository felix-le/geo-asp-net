using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thusday_lession1.Models
{
  public class Category
  {
    // in .net for pk fields, use the name {Model}Id or just Id

    public int CategoryId { get; set; }

    public string Name { get; set; }
  }
}
