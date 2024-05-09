using ProductMicroservice.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.AplicationServices.Navigation
{
    public interface IMenuAppService
    {
        List<UserMenuItem> GetMenu();
    }
}
