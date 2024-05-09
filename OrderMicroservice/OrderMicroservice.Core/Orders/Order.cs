﻿using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.Core.Orders
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [StringLength(12)]
        public string Status { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public User User { get; set; }

    }
}
