// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using ApplicationUserService.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationUserService.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/")]
public class UserController : ControllerBase
{
    private IAccountService _accountService;
    private readonly ILogger<UserController> _logger;

    public UserController(IAccountService accountService, ILogger<UserController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="RegisterDto"></param>
    /// <returns>UserDto</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Route("login")]
    public async Task<ActionResult> LoginUser([FromBody]LoginRequest request, [FromQuery] bool rememberMe)
    {
        _logger.LogInformation("POST LoginUser");
        if(await _accountService.DoesUserExistByUserNameOrEmail(email: request.Email) == false) 
            return NotFound("This user does not exist");
        try
        {
            var user = await _accountService.LoginUser(request);
            return Ok(user);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Guid">Guid of a User that data was requseted</param>
    /// <returns>UserDto</returns>
    [HttpGet]
    [Route("{userGuid:guid}")]
    public async Task<ActionResult> GetUserByGuid([FromRoute] Guid userGuid)
    {
        var temp =  await _accountService.DoesUserExistByGuid(userGuid);
        if(await _accountService.DoesUserExistByGuid(userGuid) == false) 
            return BadRequest("This user does not exist");
        
        return Ok(await _accountService.GetUserById(userGuid));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RegisterDto"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> GetAllUsers()
    {
        return Ok(await _accountService.GetAllUsers());
    }
}