using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.Navigation
{
    public static class UserMenuItemExtensions
    {
        public static bool IsMenuActive(this UserMenuItem menuItem, string currentPageName) 
        { 
            if(menuItem.Name == currentPageName)
            {
                return true;
            }
            
            if(menuItem.Items != null)
            {
                foreach (var item in menuItem.Items)
                {
                    if (item.IsMenuActive(currentPageName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
