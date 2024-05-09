using ProductMicroservice.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Client.Models
{
    public class MenuViewModel
    {
        public string CurrentPageName { get; set; }
        public List<UserMenuItem> Menu { get; set;}
    }
}
