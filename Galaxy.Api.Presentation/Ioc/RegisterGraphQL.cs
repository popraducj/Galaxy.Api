using System;
using System.Linq;
using System.Reflection;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Presentation.Authorization;
using GraphQL;
using GraphQL.Authorization;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Utilities;
using GraphQL.Validation;
using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Galaxy.Api.Presentation.Ioc
{
    public static class RegisterGraphQL
    {
        public static void AddGraphQlAuth(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.AddAuthorization(authSettings =>
            {
                var claims = Enum.GetValues(typeof(UserPermission));
                foreach (UserPermission claim in claims)
                {
                    authSettings.AddPolicy(claim.ToString(), p => p.RequireClaim(claim.ToString(), "true"));
                }
            });
        }

        public static IServiceCollection AddGraphQl(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDependencyResolver>(x =>
                new FuncDependencyResolver(x.GetRequiredService));
            services.AddAuthorization(options =>
            {
                var claims = Enum.GetValues(typeof(UserPermission));
                foreach (var claim in claims)
                {
                    options.AddPolicy(claim.ToString(), p => p.RequireClaim(claim.ToString(), "true"));
                }
            });

            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = true;
                    options.EnableMetrics = true;
                    options.ComplexityConfiguration = new ComplexityConfiguration
                    {
                        MaxDepth = 20
                    };
                })
                .AddUserContextBuilder(context =>
                {
                    try
                    {
                        return new GraphQLUserContext()
                        {
                            User = context.User,
                            UserId = context.User.Identity.Name == null? Guid.NewGuid():  Guid.Parse(context.User.Identity.Name)
                        };
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new UnauthorizedAccessException($"{ex.Message}", ex);
                    }
                    catch (Exception ex)
                    {
                        throw new UnauthorizedAccessException($"{ex.Message}", ex);
                    }
                }).AddGraphTypes(ServiceLifetime.Scoped);
            
            return services;
        }
        
         public static IServiceCollection RegisterTypes(this IServiceCollection services)
        {
           var enumGraphType = typeof(EnumerationGraphType<>);

            var coreAssembly = Assembly.Load("Galaxy.Api.Core");
            var presentationAssembly = typeof(Startup).Assembly;
            
            //add all enums
            coreAssembly.GetEnumsForPath("Galaxy.Api.Core.Enums").ForEach(p =>
            {               
                services.AddSingleton(enumGraphType.MakeGenericType(p));
                GraphTypeTypeRegistry.Register(p, enumGraphType.MakeGenericType(p));               
            });

            //add all view models
            presentationAssembly.GetTypesForPath("Galaxy.Api.Presentation.ViewModels").ForEach(p =>
            {
                services.AddScoped(p.UnderlyingSystemType);
            });
            
            //add all graphql queries and mutations
            presentationAssembly.GetTypesForPath("Galaxy.Api.Presentation.GraphQl.Types").ForEach(p =>
            {
                services.AddScoped(p.UnderlyingSystemType);
            });
          
            //add all types
            presentationAssembly.GetTypesForPath("Galaxy.Api.Presentation.GraphQl.Types.Schema").ForEach(p =>
            {
                services.AddScoped(p.GetInterfaces().First().UnderlyingSystemType, p.UnderlyingSystemType);
            });
            
            //add all mutations and queries
            presentationAssembly.GetTypesForPath("Galaxy.Api.Presentation.GraphQl.RootSchema").ForEach(p =>
            {
                if (!typeof(Schema).IsAssignableFrom(p))
                {
                    services.AddScoped(p.UnderlyingSystemType);
                }
            });

            var sp = services.BuildServiceProvider();

            //add schemas
            presentationAssembly.GetTypesForPath("Galaxy.Api.Presentation.GraphQl.RootSchema").ForEach(p =>
            {
                if (typeof(Schema).IsAssignableFrom(p))
                {
                    services.AddSingleton((ISchema) Activator.CreateInstance(p.UnderlyingSystemType,
                        new FuncDependencyResolver(type => sp.GetService(type))));
                }
            });
            return services;
        }      

    }
}