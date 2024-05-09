using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Core.Users
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public City() { Users = new List<User>(); }
    }
}
