using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Module2.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "memme")]
        public string ProductName { get; set; }
        [Required]
        [StringLength(4,MinimumLength = 2)]
        public string ProductPrice { get; set; }
    }
}
