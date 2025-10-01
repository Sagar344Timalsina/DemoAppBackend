using DemoAppBE.Domain;
using DemoAppBE.Shared;

namespace DemoAppBE.Features.Auth.Services.Interface
{
    public interface IRoleService
    {
        Task<Result> getAllRolesAsync();
        Task<Result> getRoleByIdAsync(int Id);
        Task<Result> saveRoleAsync(Role role);
        Task<Result> updateRoleAsync(Role role, int Id);
        Task<Result> DeleteRoleAsync(int Id);
    }
}
