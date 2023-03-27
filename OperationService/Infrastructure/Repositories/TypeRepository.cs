// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OperationService.DTOs;
using OperationService.Entities;
using OperationService.Exceptions;
using OperationService.Request.Response.Type;

namespace OperationService.Infrastructure.Repositories;

public interface ITypeRepository
{
    Task<TypeResponse> AddNewType(TypeDto typeDto);
    Task<TypeResponse> UpdateType(TypeDto typeDto);
    Task<TypeFamilyResponse> AddNewTypeFamily(FamilyTypeDto typeDto);
    Task<TypeFamilyResponse> UpdateFamilyType(FamilyTypeDto typeDto);
    void DisableFamilyType(int id);
    Task<TypeFamilyResponse> GetTypesByFamilyName(string familyName);
    Task<TypeFamilyResponse> GetFamilyTypeById(int id);
    Task<List<TypeFamilyResponse>> GetAllTypeFamilies();
}

public class TypeRepository : ITypeRepository
{
    private readonly DataContext.DataContext _dataContext;
    private readonly IMapper _mapper;

    public TypeRepository(DataContext.DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<TypeResponse> AddNewType(TypeDto typeDto)
    {
        var typeFamily = await _dataContext._typeFamilyEntities.FindAsync(typeDto.TypeFamilyId);
        if (typeFamily == null)
        {
            throw new NotFoundException(
                $"Not Found. Family Type Name: {typeDto.TypeFamilyName} with Id: {typeDto.TypeFamilyId}. ");
        }

        typeFamily.Types.Add(new TypeEntity()
        {
            TypeName = typeDto.TypeName
        });
        await _dataContext.SaveChangesAsync();

        return _mapper.Map<TypeResponse>(typeDto);
    }

    public async Task<TypeResponse> UpdateType(TypeDto typeDto)
    {
        var typeFamily = await _dataContext._typeFamilyEntities.FindAsync(typeDto.TypeFamilyId);
        if (typeFamily == null)
        {
            throw new NotFoundException(
                $"Not Found. Family Type Name: {typeDto.TypeFamilyName} with Id: {typeDto.TypeFamilyId}. ");
        }

        var entity = typeFamily.Types.Find(x => x.Id == typeDto.TypeId);

        if (entity == null)
        {
            throw new NotFoundException(
                $"Not Found. In Family Type Name: {typeDto.TypeFamilyName}, Type with name: {typeDto.TypeName} and id: {typeDto.TypeId}. ");
        }

        entity = _mapper.Map<TypeEntity>(typeDto);
        await _dataContext.SaveChangesAsync();

        return _mapper.Map<TypeResponse>(entity);
    }

    public async Task<TypeFamilyResponse> GetTypesByFamilyName(string familyName)
    {
        var response = await _dataContext._typeFamilyEntities.FirstOrDefaultAsync(x => x.TypeFamilyName == familyName);

        if (response is null)
        {
            throw new NotFoundException($"Not Found. Family Type Name: {familyName}. ");
        }

        return _mapper.Map<TypeFamilyResponse>(response);
    }


    public async Task<TypeFamilyResponse> AddNewTypeFamily(FamilyTypeDto typeDto)
    {
        var wallet = await _dataContext._walletEntities.FirstAsync(w => w.Id == typeDto.WalletId);
        if (wallet is null)
        {
            throw new NotFoundException($"Wallet with Id: {typeDto.WalletId} was not found");
        }

        var familyEntity = _mapper.Map<TypeFamilyEntity>(typeDto);
        familyEntity.WalletEntity = wallet;
        var response = await _dataContext._typeFamilyEntities.AddAsync(familyEntity);

        await _dataContext.SaveChangesAsync();

        return _mapper.Map<TypeFamilyResponse>(response.Entity);
    }

    public async Task<TypeFamilyResponse> UpdateFamilyType(FamilyTypeDto typeDto)
    {
        var entity = await _dataContext._typeFamilyEntities.FindAsync(typeDto.Id);
        if (entity is null)
        {
            throw new NotFoundException(
                $"Not Found. Family Type Name: {typeDto.TagFamilyName} with Id: {typeDto.Id}. ");
        }

        entity = _mapper.Map<TypeFamilyEntity>(typeDto);
        await _dataContext.SaveChangesAsync();

        return _mapper.Map<TypeFamilyResponse>(entity);
    }

    public async Task<TypeFamilyResponse> GetFamilyTypeById(int id)
    {
        var entity = await _dataContext._typeFamilyEntities.FindAsync(id);
        if (entity == null)
        {
            throw new NotFoundException($"Not Found. Family Type with Id: {id}. ");
        }

        return _mapper.Map<TypeFamilyResponse>(entity);
    }

    public async Task<List<TypeFamilyResponse>> GetAllTypeFamilies()
    {
        //todo this only needs to be used internally. Get all family types should only be correlated with wallet
        var typeFamilies = await _dataContext._typeFamilyEntities.ToListAsync();
        return _mapper.Map<List<TypeFamilyResponse>>(typeFamilies);
    }

    public async void DisableFamilyType(int id)
    {
        var entity = await _dataContext._typeFamilyEntities.FindAsync(id);
        if (entity == null)
        {
            throw new NotFoundException($"Not Found. Family Type with Id: {id}. ");
        }

        entity.IsActive = false;

        await _dataContext.SaveChangesAsync();
    }
}