using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Controllers;

[Route("api/dashboard")]
[ApiController]
[AllowAnonymous]
public class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    [HttpGet("overview")]
    public async Task<IActionResult> GetDashboardOverview()
    {
        var overviewData = await dashboardService.GetDashboardOverviewAsync();
        return Ok(overviewData);
    }
}