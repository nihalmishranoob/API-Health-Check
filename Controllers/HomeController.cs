using ApiHealthDashboard.Models;
using ApiHealthDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiHealthDashboard.Controllers;

public class HomeController : Controller
{
    private readonly HealthCheckService _healthCheckService;

    public HomeController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new IndexViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(HealthCheckRequest request)
    {
        var vm = new IndexViewModel { Request = request };

        if (string.IsNullOrWhiteSpace(request.Url))
        {
            vm.ErrorMessage = "Please enter a valid URL.";
            return View(vm);
        }

        vm.Result = await _healthCheckService.CheckAsync(request);
        return View(vm);
    }
}
