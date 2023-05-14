using Simple.Core.Domain;
using Simple.Core.Localization;

namespace Simple.Authorization.Services
{
    public abstract class ServiceBase : AppServiceBase
    {
        protected ServiceBase() : base(AppsettingConfig.GetConnectionString("DBConnection"))
        {

        }
    }
}
