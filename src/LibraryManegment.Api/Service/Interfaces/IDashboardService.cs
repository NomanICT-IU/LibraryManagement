using LibraryManegment.Api.Dtos.DashBoardDtos;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IDashboardService
{
    Task<DashboardOverviewDto> GetDashboardOverviewAsync();
}
