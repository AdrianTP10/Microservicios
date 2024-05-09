using OrderMicroservice.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.AplicationServices.Navigation
{
    public interface IMenuAppService
    {
        List<UserMenuItem> GetMenu();
    }
}
