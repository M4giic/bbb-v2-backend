using AutoMapper;
using OperationService.Request;
using Microsoft.AspNetCore.Mvc;
using OperationService.Application;
using OperationService.DTOs;
using OperationService.Request.Request.Operation;

namespace OperationService.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OperationController : ControllerBase
{
    private readonly IAtomicOperationService _atomicOperationService;
    private readonly IMapper _mapper;

    public OperationController(IAtomicOperationService atomicOperationService, IMapper mapper)
    {
        _atomicOperationService = atomicOperationService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="operationId"></param>
    /// <returns>OperationDto</returns>
    [HttpGet]
    [Route("{operationId:int}")]
    public async Task<IActionResult> GetOperationById(int operationId)
    {
        return Ok(await _atomicOperationService.GetOperationById(operationId));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletGuid"></param>
    /// <returns>List of OperationDtos</returns>
    [HttpGet]
    [Route("/wallet/{walletGuid:guid}")]
    public async Task<IActionResult> GetWalletsByOwnerGuid(Guid walletGuid)
    {
        return Ok(await _atomicOperationService.GetOperationsByWalletGuid(walletGuid));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Id of created operation</returns>
    [HttpPost]
    public async Task<IActionResult> AddOperation([FromBody] AddOperationRequest request)
    {
        return Ok(await _atomicOperationService.AddOperation(_mapper.Map<OperationDto>(request)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletGuid"></param>
    /// <param name="request"></param>
    /// <returns>WalletDto</returns>
    [HttpPut("{operationId:int}")]
    public async Task<IActionResult> UpdateOperation(int operationId, [FromBody] UpdateOperationRequest request)
    {
        request.Id = operationId;
        return Ok(await _atomicOperationService.UpdateOperation(_mapper.Map<OperationDto>(request)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletGuid"></param>
    /// <param name="force"></param>
    [HttpDelete("{operationId:int}")]
    public async Task<IActionResult> DeleteOperation(int operationId, [FromQuery] bool force)
    {
        await _atomicOperationService.DeleteOperationById(operationId, force);
        return NoContent();
    }
}