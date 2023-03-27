using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OperationService.Application;
using OperationService.DTOs;
using OperationService.Request;
using OperationService.Request.Request.Wallet;

namespace OperationService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;

    public WalletController(IWalletService walletService, IMapper mapper)
    {
        _walletService = walletService;
        _mapper = mapper;
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletGuid"></param>
    /// <returns>WalletDto</returns>
    [HttpGet]
    [Route("{walletGuid:guid}")]
    public async Task<IActionResult> GetWalletByGuid(Guid walletGuid)
    {
        return Ok(await _walletService.GetWalletByGuid(walletGuid));
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletGuid"></param>
    /// <returns>List of WalletDtos</returns>
    [HttpGet]
    [Route("owner")]
    public async Task<IActionResult> GetWalletsByOwnerGuid()
    {
        if (Request.Headers.TryGetValue("x-user-id", out var ownerGuid) == false)
        {
            return BadRequest("User Id was not provided");
        }
        return Ok(await _walletService.GetWalletsByOwnerGuid(Guid.Parse(ownerGuid)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Guid of created wallet</returns>
    [HttpPost]
    public async Task<IActionResult> AddWallet([FromBody] AddWalletRequest request)
    {
        return Ok(await _walletService.AddWallet(_mapper.Map<WalletDto>(request)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletGuid"></param>
    /// <param name="request"></param>
    /// <returns>WalletDto</returns>
    [HttpPut("{walletGuid:guid}")]
    public async Task<IActionResult> Put(Guid walletGuid, [FromBody] UpdateWalletRequest request)
    {
        request.Id = walletGuid;
        return Ok(await _walletService.UpdateWallet(_mapper.Map<WalletDto>(request)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletGuid"></param>
    /// <param name="force"></param>
    [HttpDelete("{walletGuid:guid}")]
    public async Task<IActionResult> DeleteWallet(Guid walletGuid, [FromQuery] bool force)
    {
        await _walletService.DeleteWallet(walletGuid, force);
        return NoContent();
    }
}