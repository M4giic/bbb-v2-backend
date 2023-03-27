// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using ApplicationUserService.Infrastructure;
using ApplicationUserService.Service;

namespace ApplicationUserService;

public static class ApplicatoinServiceExtension
{
    
    public static IServiceCollection AddApplicationSpecificServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<ISingleSignOnTokenRepository, SingleSignOnTokenRepository>()
            .AddSingleton<ITokenService, TokenService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IAccountService, AccountService>();
        
        return serviceCollection;
    }
}