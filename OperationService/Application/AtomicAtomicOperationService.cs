
using AutoMapper;
using OperationService.DTOs;
using OperationService.Infrastructure.Repositories;

namespace OperationService.Application;

public class AtomicAtomicOperationService : IAtomicOperationService
{
    private readonly IAtomicOperationRepository _atomicAtomicOperationRepository;
    
    public AtomicAtomicOperationService(IAtomicOperationRepository atomicAtomicOperationRepository)
    {
        _atomicAtomicOperationRepository = atomicAtomicOperationRepository;
    }

    public async Task<OperationDto> GetOperationById(int id)
    {
        return await _atomicAtomicOperationRepository.GetOperationById(id);
    }

    public async Task<int> AddOperation(OperationDto operationDto)
    {
        return await _atomicAtomicOperationRepository.AddOperation(operationDto);
    }

    public async Task<OperationDto> UpdateOperation(OperationDto operationDto)
    {
        return await _atomicAtomicOperationRepository.UpdateOperation(operationDto);
    }

    public async Task<int> DeleteOperationById(int id, bool force)
    {
        return await _atomicAtomicOperationRepository.DeleteOperationByIdAsync(id,force);
    }

    public async Task<List<OperationDto>> GetOperationsByWalletGuid(Guid walletGuid)
    {
        return await _atomicAtomicOperationRepository.GetAllOperationsByWalletGuid(walletGuid);
    }
}