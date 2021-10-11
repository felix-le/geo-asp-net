using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thusday_lession1.Models
{
  public class Pet
  {
    public int PetId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int CategoryId { get; set; }

    //Add parent ref to Category (1 category => many Pets)
    public Category Category { get; set; }

    //Add child ref's (1 pet => Many CartItems / 1 pet => Many orderDetails)

    public List<CartItem> CardItems { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
  }
}
