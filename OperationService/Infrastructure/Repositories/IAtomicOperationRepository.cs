using OperationService.DTOs;

namespace OperationService.Infrastructure.Repositories;

public interface IAtomicOperationRepository
{
    Task<OperationDto> GetOperationById(int id);
    Task<int> AddOperation(OperationDto operationDto);
    Task<OperationDto> UpdateOperation(OperationDto operationDto);
    Task<int> DeleteOperationByIdAsync(int id, bool force = false);
    Task<List<OperationDto>> GetAllOperationsByWalletGuid(Guid walletGuid);
}