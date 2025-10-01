using DemoAppBE.Shared;

namespace DemoAppBE.Infrastructure.Security
{
    public interface IPasswordHasher:IService
    {
        string Hash(string password);

        bool Verify(string password, string passwordHash);
    }
}
