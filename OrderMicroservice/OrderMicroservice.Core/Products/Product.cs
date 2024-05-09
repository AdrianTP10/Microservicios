using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.Core.Products
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Description { get; set; }
        [Required]
        [StringLength(17)]
        public string Brand { get; set; }
        [Required]
        [StringLength(15)]
        public string Category { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

    }
}
