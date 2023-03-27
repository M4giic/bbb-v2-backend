using OperationService.DTOs;

namespace OperationService.Application;

public interface IAtomicOperationService
{
    Task<OperationDto> GetOperationById(int id);
    Task<int> AddOperation(OperationDto operationDto);
    Task<OperationDto> UpdateOperation(OperationDto operationDto);
    Task<int> DeleteOperationById(int id, bool force = false);
    Task<List<OperationDto>> GetOperationsByWalletGuid(Guid walletGuid);
}