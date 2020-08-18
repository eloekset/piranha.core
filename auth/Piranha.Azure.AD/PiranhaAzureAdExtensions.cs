/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/piranhacms/piranha.core
 *
 */

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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
    public static PiranhaServiceBuilder UseAzureAD(this PiranhaServiceBuilder serviceBuilder, System.Action<AzureADOptions> azureADOptions, System.Action<OpenIdConnectOptions> openIdConnectOptions)
    {
        serviceBuilder.Services.AddPiranhaAzureAD(azureADOptions, openIdConnectOptions);

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
    public static IServiceCollection AddPiranhaAzureAD(this IServiceCollection services, System.Action<AzureADOptions> azureADOptions, System.Action<OpenIdConnectOptions> openIdConnectOptions)
    {
        // Add the identity module
        App.Modules.Register<Module>();
        // Register the auth service
        services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            .AddAzureAD(azureADOptions);
        services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, openIdConnectOptions);

        return services;
    }
}