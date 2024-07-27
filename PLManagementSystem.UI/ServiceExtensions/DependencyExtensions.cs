using PLManagementSystem.Core.Interfaces.IWrapper;
using PLManagementSystem.Data.Wrapper;
using System.Reflection;

namespace PLManagementSystem.UI.ServiceExtensions
{
    public static class DependencyExtensions
    {
        public static void ConfigureAPIBrokereDependencyExtensions(IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDataWrapper, DataWrapper>();
        }
        // inject all service 
        public static void InjectService(this IServiceCollection services, params Assembly[] assemblies)
        {


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
