using PLManagementSystem.Core.Interfaces.IWrapper;
using PLManagementSystem.Data.Wrapper;
using System.Reflection;

namespace PLManagementSystem.UI.ServiceExtensions
{
    public static class DependencyExtensions
    {
        public static void ConfigureDependencyExtensions(IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDataWrapper, DataWrapper>();
            foreach (var implementationType in assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => !type.GetTypeInfo().IsAbstract))
            {
                foreach (var interfaceType in implementationType.GetInterfaces())
                {
                    if (interfaceType.Name.EndsWith("Service"))
                        services.AddTransient(interfaceType, implementationType);
                }
            }
        }
    }
}
