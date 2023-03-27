// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OperationService.DTOs;
using OperationService.Entities;

namespace OperationService.Infrastructure.Repositories;

public class AtomicAtomicOperationRepository : IAtomicOperationRepository
{
    private readonly DataContext.DataContext _dataContext;
    private readonly IMapper _mapper;
    
    public AtomicAtomicOperationRepository(DataContext.DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<OperationDto> GetOperationById(int id)
    {
        var operationEntity = await _dataContext._operationEntities.FindAsync(id);

        return _mapper.Map<OperationDto>(operationEntity);
    }

    public async Task<int> AddOperation(OperationDto operationDto)
    {
        var walletEntity = await _dataContext._walletEntities.FindAsync(operationDto.WalletId);
        var operationEntity = _mapper.Map<OperationEntity>(operationDto);
        operationEntity.Wallet = walletEntity;
        await _dataContext._operationEntities.AddAsync(operationEntity);

        await _dataContext.SaveChangesAsync();

        return operationEntity.Id;
    }

    public async Task<OperationDto> UpdateOperation(OperationDto operationDto)
    {
        var operationEntity = await _dataContext._operationEntities.FindAsync(operationDto.Id);

        operationEntity = _mapper.Map<OperationEntity>(operationDto);

        await _dataContext.SaveChangesAsync();

        return _mapper.Map<OperationDto>(operationEntity);
    }

    public async Task<int> DeleteOperationByIdAsync(int id, bool force = false)
    {
        var operationEntity = await _dataContext._operationEntities.FindAsync(id);
        _dataContext._operationEntities.Remove(operationEntity);

        return await _dataContext.SaveChangesAsync();
    }

    public async Task<List<OperationDto>> GetAllOperationsByWalletGuid(Guid walletGuid)
    {
        var operationList = await _dataContext._operationEntities.Where(x => x.WalletId == walletGuid).ToListAsync();
        return _mapper.Map<List<OperationDto>>(operationList);
    }
}