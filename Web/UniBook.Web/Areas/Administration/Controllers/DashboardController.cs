﻿namespace UniBook.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using UniBook.Services.Data;
    using UniBook.Web.Areas.Administration.ViewModels.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = await this.settingsService.GetCountAsync(), };
            return this.View(viewModel);
        }
    }
}
