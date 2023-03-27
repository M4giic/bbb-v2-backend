// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using OperationService.Infrastructure.Repositories;

namespace OperationService.Extension;

public static class InfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IWalletRepository, WalletRepository>()
            .AddScoped<ITypeRepository, TypeRepository>()
            .AddScoped<IAtomicOperationRepository, AtomicAtomicOperationRepository>();
    }
}