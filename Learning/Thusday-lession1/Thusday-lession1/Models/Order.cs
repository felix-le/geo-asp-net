using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thusday_lession1.Models
{
  public class Order
  {
    public int OrderId
    {
      get; set;
    }

    public DateTime OrderDate { get; set; }

    public decimal OrderTotal { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [MaxLength(2)]
    public string Province { get; set; }

    [Required]
    public string CustomerId { get; set; }
  }
}
