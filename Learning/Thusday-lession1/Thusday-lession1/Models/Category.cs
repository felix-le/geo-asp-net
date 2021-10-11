using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thusday_lession1.Models
{
  public class Category
  {
    // in .net for pk fields, use the name {Model}Id or just Id
    [Range(1, 99999)]
    [Display(Name = "Category Id")]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Hey, I'm Here!!!!")]
    [Display(Name = "Please give my name!!!")]

    public string Name { get; set; }

    //Ref to child model: 1 Category => many Pets
    public List<Pet> Pets { get; set; }
  }
}
