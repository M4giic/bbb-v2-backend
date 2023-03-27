// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using ApplicationUserService.Domain.Dto;
using ApplicationUserService.Service;
using AutoMapper;
using BBBv2.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationUserService.Controllers;

[ApiController]
[Route("api/")]
public class RegisterController : ControllerBase
{
    private IAccountService _accountService;
    private readonly int _tokenLength;
    private IMapper _mapper;

    public RegisterController(IAccountService accountService, IMapper mapper, IConfiguration configuration)
    {
        _accountService = accountService;
        _mapper = mapper;
        _tokenLength = configuration.GetValue<int>("KeyConfiguration:SingleSignOnTokenLenght");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns>UserDto</returns>
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> RegisterNewUser(RegisterUserRequest request)
    {
        //create password hash
        //send an email
        if(await _accountService.DoesUserExistByUserNameOrEmail(request.Username, request.EmailAddress)) 
            return BadRequest("This username or email is already taken");
            
        var newUser = await _accountService.RegisterNewUser(_mapper.Map<AccountDto>(request));

        return Ok(newUser);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="RegisterDto"></param>
    /// <returns>UserDto</returns>
    [HttpPost]
    [Route("validate/{accountGuid:guid}=={tokenValue}")]
    public async Task<ActionResult> ValidateUserViaEmail(Guid accountGuid, string tokenValue)
    {
        //is used after receving an email
        
        if(await _accountService.DoesUserExistByGuid(accountGuid) == false) 
            return BadRequest("User doesn't exist");

        if (await _accountService.ActivateAccountUsingSingleSignOnToken(accountGuid, tokenValue))
            return NoContent();
        
        return BadRequest("Something went wrong");
    }
    
    /// <summary>
    /// Used to update user status like retiring or upgrading access
    /// </summary>
    /// <param name="RegisterDto"></param>
    /// <returns>UserDto</returns>
    [HttpDelete]
    [Route("delete/{accountGuid:guid}")]
    public async Task<ActionResult> DeleteUser(Guid accountGuid)
    {
        if(await _accountService.DoesUserExistByGuid(accountGuid) == false) 
            return BadRequest("User doesn't exist");
        
        return Ok(await _accountService.UpdateUserStatus(accountGuid, AccountStatus.InActive));
    }
}