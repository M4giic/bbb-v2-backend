// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using AutoMapper;
using OperationService.Models;
using OperationService.DTOs;
using OperationService.Entities;
using OperationService.Infrastructure.Repositories;

namespace OperationService.Application;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public WalletService(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddWallet(WalletDto walletDto)
    {
        return await _walletRepository.AddWalletAsync(_mapper.Map<WalletEntity>(walletDto));
    }

    public async Task<WalletDto> GetWalletByGuid(Guid walletGuid)
    {
        var wallet = await _walletRepository.GetWalletByGuid(walletGuid);
        return _mapper.Map<WalletDto>(wallet);
    }
    
    public async Task<List<WalletDto>> GetWalletsByOwnerGuid(Guid ownerGuid)
    {
        var wallet = await _walletRepository.GetWalletsByOwnerGuid(ownerGuid);
        return _mapper.Map<List<WalletDto>>(wallet);
    }

    public async Task<WalletDto>  UpdateWallet(WalletDto walletDto)
    {
        var wallet =  await _walletRepository.UpdateWallet(_mapper.Map<WalletEntity>(walletDto));
        return _mapper.Map<WalletDto>(wallet);
    }

    public async Task DeleteWallet(Guid walletGuid, bool force = false)
    {
        await _walletRepository.DeleteWalletByGuid(walletGuid, force);
    }

}