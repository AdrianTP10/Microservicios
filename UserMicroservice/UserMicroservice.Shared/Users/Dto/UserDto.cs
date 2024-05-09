using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMicroservice.Users.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        [Required, StringLength(12)] public string Role { get; set; }

        public int CityId { get; set; }
        
        public DateTime Birthday { get; set; }
    }
}
