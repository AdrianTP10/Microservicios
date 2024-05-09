using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.ProductsDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string WrapperType { get; set; }
        [Required]
        [StringLength(30)]
        public string DescriptionProduct { get; set; }
        [Required]
        [StringLength(17)]
        public string Brand { get; set; }
        [Required]
        [StringLength(15)]
        public string CategoryProduct { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double FinalPrice { get; set; }
    }
}
