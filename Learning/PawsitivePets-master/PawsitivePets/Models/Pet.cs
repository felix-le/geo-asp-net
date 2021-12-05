using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PawsitivePets.Models
{
    public class Pet
    {
        public int PetId { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Colour { get; set; }

        [Required]
        [Range(0, 99)]
        public int Age { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]  // c represents MS currency format
        public decimal Price { get; set; }

        public string Photo { get; set; }

        // Add parent ref to Category (1 Category => Many Pets)
        public Category Category { get; set; }

        // Add child ref's (1 pet => Many CartItems / 1 pet => Many OrderDetails)
        public List<CartItem> CartItems { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
