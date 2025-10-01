using DemoAppBE.Domain;
using DemoAppBE.Features.Auth.Services.Interface;
using DemoAppBE.Shared;

namespace DemoAppBE.Features.Auth.Services.Implementation
{
    public class RoleService : IRoleService
    {
        public async Task<Result> DeleteRoleAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> getAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> getRoleByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> saveRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> updateRoleAsync(Role role, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
