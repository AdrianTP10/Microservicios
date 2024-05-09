using ProductMicroservice.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.AplicationServices.Navigation
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
                        Url = "/Products/Create",
                    },
                    new UserMenuItem
                    {
                        Name = "Edit",
                        DisplayName = "Edit",
                        Order = 1,
                        Url = "#",
                    },
                    new UserMenuItem
                    {
                        Name = "Delete",
                        DisplayName = "Delete",
                        Order = 2,
                        Url = "#",
                    }
                }
            });
            menuItems.Add(new UserMenuItem
            {
                Name = "Sales",
                DisplayName = "Sales",
                Order = 2,
                Url = "#",
                Items = new List<UserMenuItem>()
                {
                    new UserMenuItem
                    {
                        Name = "GetBestSale",
                        DisplayName = "GetBestSale",
                        Order = 0,
                        Url = "#",
                    },
                    new UserMenuItem
                    {
                        Name = "ProductSale",
                        DisplayName = "ProductSale",
                        Order = 1,
                        Url = "#",
                    }
                }
            });
            menuItems.Add(new UserMenuItem
            {
                Name = "Supplier",
                DisplayName = "Supplier",
                Order = 3,
                Url = "#",
                Items = new List<UserMenuItem>()
                {
                    new UserMenuItem
                    {
                        Name = "GetSuppliers",
                        DisplayName = "GetSuppliers",
                        Order = 0,
                        Url = "#",
                    }
                }
            });
            menuItems.Add(new UserMenuItem
            {
                Name = "Replenish",
                DisplayName = "Replenish",
                Order = 4,
                Url = "#",
                Items = new List<UserMenuItem>()
                {
                    new UserMenuItem
                    {
                        Name = "ReplenishProduct",
                        DisplayName = "Replenish Product",
                        Order = 0,
                        Url = "#"
                    }
                }
            });
            menuItems.Add(new UserMenuItem
            {
                Name = "Reports",
                DisplayName = "Reports",
                Order = 5,
                Url = "#",
                Items = new List<UserMenuItem>()
                {
                    new UserMenuItem
                    {
                        Name = "Inventory",
                        DisplayName = "Inventory",
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
