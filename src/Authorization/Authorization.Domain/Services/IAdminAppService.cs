using Simple.dotNet.Core.Dependency;

namespace Authorization.Domain.Services
{
    public interface IAdminAppService : ISingletonDependency
    {
        string Test();
    }
}
