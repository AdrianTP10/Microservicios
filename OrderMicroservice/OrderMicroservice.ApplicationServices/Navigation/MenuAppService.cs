using OrderMicroservice.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.AplicationServices.Navigation
{
    public class MenuAppService : IMenuAppService
    {
        public List<UserMenuItem> GetMenu()
        {
            List<UserMenuItem> menuItems = new List<UserMenuItem>();
            menuItems.Add(new UserMenuItem
            {
                Name = "Administration",
                DisplayName = "Administration",
                Order = 1,
                Url = "#",
                Items = new List<UserMenuItem>()
                {
                    new UserMenuItem
                    {
                        Name = "Create",
                        DisplayName = "Create",
                        Order = 0,
                        Url = "/Orders/Create",
                    },
                    new UserMenuItem
                    {
                        Name = "CreateWithProduct",
                        DisplayName = "Create With Product",
                        Order = 1,
                        Url = "/Orders/CreateWithProduct",
                    },
                    new UserMenuItem
                    {
                        Name = "CreateWithUser",
                        DisplayName = "Create With User",
                        Order = 2,
                        Url = "/Orders/CreateWithUser",
                    },
                    new UserMenuItem
                    {
                        Name = "CreateWithUserWithCity",
                        DisplayName = "Create With User With City",
                        Order = 3,
                        Url = "/Orders/CreateWithUserWithCity",
                    },
                    new UserMenuItem
                    {
                        Name = "CreateComplete",
                        DisplayName = "Create Complete",
                        Order = 4,
                        Url = "/Orders/CreateComplete",
                    },
                    new UserMenuItem
                    {
                        Name = "Edit",
                        DisplayName = "Edit",
                        Order = 5,
                        Url = "#",
                    },
                    new UserMenuItem
                    {
                        Name = "Delete",
                        DisplayName = "Delete",
                        Order = 6,
                        Url = "#",
                    }
                }
            });
            menuItems.Add(new UserMenuItem
            {
                Name = "Orders",
                DisplayName = "Orders",
                Order = 2,
                Url = "#",
                Items = new List<UserMenuItem>()
                {
                    new UserMenuItem
                    {
                        Name = "Pendents",
                        DisplayName = "Pendents",
                        Order = 0,
                        Url = "#",
                    },
                    new UserMenuItem
                    {
                        Name = "Delivered",
                        DisplayName = "Delivered",
                        Order = 1,
                        Url = "#",
                    }
                }
            });
            menuItems.Add(new UserMenuItem
            {
                Name = "Reports",
                DisplayName = "Reports",
                Order = 3,
                Url = "#",
                Items = new List<UserMenuItem>()
                {
                    new UserMenuItem
                    {
                        Name = "Delivered",
                        DisplayName = "Delivered",
                        Order = 0,
                        Url = "#",
                    },
                    new UserMenuItem
                    {
                        Name = "Sales",
                        DisplayName = "Sales",
                        Order = 1,
                        Url = "#",
                    }
                }
            });
            return menuItems;
        }
    }
}
