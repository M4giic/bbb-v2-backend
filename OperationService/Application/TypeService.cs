// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using AutoMapper;
using OperationService.DTOs;
using OperationService.Infrastructure.Repositories;
using OperationService.Request.Request.Type;
using OperationService.Request.Response.Type;

namespace OperationService.Application;

public interface ITypeService
{
    Task<TypeResponse> AddNewType(AddNewTypeRequest request);
    Task<TypeResponse> UpdateType(UpdateTypeRequest request);
    Task<TypeFamilyResponse> AddNewTypeFamily(AddNewTypeFamilyRequest request);
    Task<TypeFamilyResponse> UpdateTypeFamily(UpdateTypeFamilyRequest request);
    Task<TypeFamilyResponse> GetTypesByTypesFamilyName(string familyTypeName);
    Task<TypeFamilyResponse> GetTypesByTypesFamilyId(int familyTypeId);
    Task<List<TypeFamilyResponse>> GetAllTypesFamilies();
    void DisableFamilyType(int id);
}

public class TypeService : ITypeService
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ITypeService> _logger;

    public TypeService(ITypeRepository typeRepository, IMapper mapper, ILogger<TypeService> logger)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TypeResponse> AddNewType(AddNewTypeRequest request)
    {
        _logger.LogInformation($"Add new type: {request}");
        return await _typeRepository.AddNewType(_mapper.Map<TypeDto>(request));
    }

    public async Task<TypeResponse> UpdateType(UpdateTypeRequest request)
    {
        _logger.LogInformation($"Update type: {request}");
        return await _typeRepository.UpdateType(_mapper.Map<TypeDto>(request));
    }
    public async Task<TypeFamilyResponse> AddNewTypeFamily(AddNewTypeFamilyRequest request)
    {
        _logger.LogInformation($"Add new type family: {request}");
        return await _typeRepository.AddNewTypeFamily(_mapper.Map<FamilyTypeDto>(request));
    }
    
    public async Task<TypeFamilyResponse> UpdateTypeFamily(UpdateTypeFamilyRequest request)
    {
        _logger.LogInformation($"Update type family: {request}");
        return await _typeRepository.UpdateFamilyType(_mapper.Map<FamilyTypeDto>(request));
    }

    
    public async Task<TypeFamilyResponse> GetTypesByTypesFamilyName(string familyTypeName)
    {
        _logger.LogInformation($"Get type family by name: {familyTypeName}");
        return await _typeRepository.GetTypesByFamilyName(familyTypeName);
    }

    public async Task<TypeFamilyResponse> GetTypesByTypesFamilyId(int familyTypeId)
    {
        _logger.LogInformation($"Get type family by Id: {familyTypeId}");
        return await _typeRepository.GetFamilyTypeById(familyTypeId);
    }
    
    public async Task<List<TypeFamilyResponse>> GetAllTypesFamilies()
    {
        _logger.LogInformation("Get all type families");
        return await _typeRepository.GetAllTypeFamilies();
    }

    public void DisableFamilyType(int id)
    {
        _typeRepository.DisableFamilyType(id);
    }
}