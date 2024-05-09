using OrderMicroservice.AplicationServices.Navigation;
using OrderMicroservice.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Client.Pages.Shared.Components.AppMenu
{
    public class AppMenuViewComponent : ViewComponent
    {
        private readonly IMenuAppService _menuAppService;

        public AppMenuViewComponent(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string currentPageName = null)
        {
            var model = new MenuViewModel
            {
                CurrentPageName = currentPageName,
                Menu = _menuAppService.GetMenu()
            };

            return View("Default", model);
        }
    }
}
