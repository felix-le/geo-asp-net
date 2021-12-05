using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PawsitivePets.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int PetId { get; set; }

        // parent ref (1 Pet => Many OrderDetails)
        public Pet Pet { get; set; }

        // parent ref (1 Order => Many OrderDetails)
        public Order Order { get; set; }
    }
}
