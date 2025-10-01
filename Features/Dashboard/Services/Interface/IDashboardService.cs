using DemoAppBE.Shared;

namespace DemoAppBE.Features.Dashboard.Services.Interface
{
    public interface IDashboardService:IService
    {
        public Task<Result> getAllDashboardDataAsync();
    }
}
