// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using OperationService.Application;

namespace OperationService.Extension;

public static class ApplicationExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IWalletService, WalletService>()
            .AddScoped<IAtomicOperationService,Application.AtomicAtomicOperationService>()
            .AddScoped<ITypeService,TypeService>();
            
    }

}