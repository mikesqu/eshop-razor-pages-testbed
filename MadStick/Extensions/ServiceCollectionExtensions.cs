using MadStickWebAppTester.Authorization.Handlers;
using MadStickWebAppTester.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace MadStickWebAppTester.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //products
                options.AddPolicy(
                    "CanModifyProducts",
                    policyBuilder => policyBuilder.AddRequirements(new CanModifyEndpointRequirement("/Products")));

                //storage-products
                options.AddPolicy(
                    "CanModifyStorageProducts",
                    policyBuilder => policyBuilder.AddRequirements(new CanModifyEndpointRequirement("/StorageProducts")));
                options.AddPolicy(
                    "CanViewStorageProducts",
                    policyBuilder => policyBuilder.AddRequirements(new CanAccessEndpointRequirement("/StorageProducts")));

                //storage-units
                options.AddPolicy(
                    "CanModifyStorageUnits",
                    policyBuilder => policyBuilder.AddRequirements(new CanModifyEndpointRequirement("/StorageUnits")));
                options.AddPolicy(
                    "CanViewStorageUnits",
                    policyBuilder => policyBuilder.AddRequirements(new CanAccessEndpointRequirement("/StorageUnits")));

                //carts
                options.AddPolicy(
                    "CanModifyCarts",
                    policyBuilder => policyBuilder.AddRequirements(new CanModifyEndpointRequirement("/Carts")));
                options.AddPolicy(
                    "CanViewCarts",
                    policyBuilder => policyBuilder.AddRequirements(new CanAccessEndpointRequirement("/Carts")));

                    options.AddPolicy(
                        "CanViewCartDetails",
                        policyBuilder => policyBuilder.AddRequirements(new CanAccessEndpointRequirement("/Carts/Details")));

                //cart-products
                options.AddPolicy(
                    "CanModifyCartProducts",
                    policyBuilder => policyBuilder.AddRequirements(new CanModifyEndpointRequirement("/CartProducts")));
                options.AddPolicy(
                    "CanViewCartProducts",
                    policyBuilder => policyBuilder.AddRequirements(new CanAccessEndpointRequirement("/CartProducts")));

                //users
                options.AddPolicy(
                    "CanModifyUsers",
                    policyBuilder => policyBuilder.AddRequirements(new CanModifyEndpointRequirement("/Users")));
                options.AddPolicy(
                    "CanViewUsers",
                    policyBuilder => policyBuilder.AddRequirements(new CanAccessEndpointRequirement("/Users")));
            });

            services.AddScoped<IAuthorizationHandler, IsAdminHandler>();
            services.AddScoped<IAuthorizationHandler, IsSalesManagerHandler>();
            services.AddScoped<IAuthorizationHandler, IsCartOwnerHandler>();

            return services;
        }
    }
}
