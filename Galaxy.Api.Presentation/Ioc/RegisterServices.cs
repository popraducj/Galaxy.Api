using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Api.Presentation.Ioc
{
    public  static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            AddServices("Galaxy.Api.Core", "Galaxy.Api.Core.Services", services);
            AddServices("Galaxy.Api.Infrastructure", "Galaxy.Api.Infrastructure.Grpc.Services", services);
            return services;
        }

        private static void AddServices(string assemblyName, string path, IServiceCollection services)
        {
            var dataAssembly = Assembly.Load(assemblyName);

            dataAssembly.GetTypesForPath(path)
                .ForEach(p =>
                {
                    var interfaceValue = p.GetInterfaces().FirstOrDefault();

                    if (interfaceValue != null)
                    {
                        services.AddScoped(interfaceValue.UnderlyingSystemType, p.UnderlyingSystemType);
                    }
                });
        }
    }
}