using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thusday_lession1.Models
{
  public class Oder
  {
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal OrderTotal { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string CustomerId { get; set; }
  }
}
