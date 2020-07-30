/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/piranhacms/piranha.core
 *
 */

using Microsoft.Extensions.DependencyInjection;
using Piranha;
using Piranha.Azure.AD;

public static class PiranhaSearchExtensions
{
    /// <summary>
    /// Adds the Azure AD module.
    /// </summary>
    /// <param name="serviceBuilder">The service builder</param>
    /// <param name="serviceName">The unique name of the azure search service</param>
    /// <param name="apiKey">The admin api key</param>
    /// <returns>The services</returns>
    public static PiranhaServiceBuilder UseAzureAD(this PiranhaServiceBuilder serviceBuilder,
        string tenantId, string clientId, string clientSecret, string redirectUri)
    {
        serviceBuilder.Services.AddPiranhaAzureAD(tenantId, clientId, clientSecret, redirectUri);

        return serviceBuilder;
    }

    /// <summary>
    /// Adds the Azure AD module.
    /// </summary>
    /// <param name="services">The current service collection</param>
    /// <param name="tenantId">The Azure AD tenant used to authenticate users</param>
    /// <param name="clientId">The application ID registered in Azure AD</param>
    /// <param name="redirectUri">The redirect URI to handle authentication callback</param>
    /// <returns>The services</returns>
    public static IServiceCollection AddPiranhaAzureAD(this IServiceCollection services,
        string tenantId, string clientId, string clientSecret, string redirectUri)
    {
        // Add the identity module
        App.Modules.Register<Module>();

        // Register the auth service
        services.AddAuthentication()
            .AddOAuth("Cookie", options =>
            {
                options.AuthorizationEndpoint = $"https://login.microsoftonline.com/{tenantId}";
                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
                options.CallbackPath = redirectUri;
            });

        return services;
    }
}