using DemoAppBE.Domain;
using DemoAppBE.Shared;

namespace DemoAppBE.Infrastructure.Security
{
    public interface ITokenProvider:IService
    {
        string CreateToken(User user);
        string CreateRefereshToken();
    }
}
