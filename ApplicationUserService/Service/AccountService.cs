// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using System.Security.Cryptography;
using System.Text;
using ApplicationUserService.Infrastructure;
using AutoMapper;
using BBBv2.Infrastructure.Entities;

namespace ApplicationUserService.Service;
public interface IAccountService
{
    Task<bool> DoesUserExistByUserName(string userName);
    Task<bool> DoesUserExistByUserNameOrEmail(string userName = "", string email = "");
    Task<UserDto> LoginUser(LoginRequest request);
    Task<UserDto> RegisterNewUser(AccountDto accountToAdd);
    Task<bool> ActivateAccountUsingSingleSignOnToken(Guid accountGuid, string tokenValue);
    Task<bool> DoesUserExistByGuid(Guid accountGuid);
    Task<AccountInformationDto> UpdateUserStatus(Guid userGuid, AccountStatus status);
    Task<AccountInformationDto> GetUserById(Guid userGuid);
    Task<List<AccountInformationDto>> GetAllUsers();
}
internal class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ISingleSignOnTokenRepository _singleSignOnTokenRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public AccountService(IAccountRepository accountRepository, IMapper mapper, ITokenService tokenService, ISingleSignOnTokenRepository singleSignOnTokenRepository, IEmailService emailService)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _tokenService = tokenService;
        _singleSignOnTokenRepository = singleSignOnTokenRepository;
        _emailService = emailService;
    }
    public async Task<bool> DoesUserExistByUserName(string userName)
    {
        return await _accountRepository.DoesUserExistByUsername(userName);
    }

    public async Task<bool> DoesUserExistByUserNameOrEmail(string userName = "", string email = "" )
    {
        return (await _accountRepository.DoesUserExistByUsername(userName) || await _accountRepository.DoesUserExistByEmail(email));
    }
    
    public async Task<bool> DoesUserExistByGuid(Guid accountGuid)
    {
        var ret = await _accountRepository.DoesUserExistByGuid(accountGuid);
        return ret;
    }

    public async Task<UserDto> LoginUser(LoginRequest request)
    {
        // var account = await _accountRepository.GetUserByUsername(request.Username);
        var account = await _accountRepository.GetUserByEmail(request.Email);
        using var hmac = new HMACSHA512();
        hmac.Key = account.PasswordSalt;
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        
        if (account.PasswordHash.Length != computedHash.Length)
            throw new BadHttpRequestException("Incorrect password");
        
        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != account.PasswordHash[i])
                throw new BadHttpRequestException("Incorrect password");
        }
        
        return new UserDto{
            Username = account.Username,
            Token = _tokenService.CreateToken(_mapper.Map<AccountDto>(account)),
            Id = account.Id
        };
    }

    public async Task<UserDto> RegisterNewUser(AccountDto accountToAdd)
    {
        using var hmac = new HMACSHA512();

        var account = new AccountEntity()
        {
            Username = accountToAdd.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(accountToAdd.Password)),
            PasswordSalt = hmac.Key,
            EmailAddress = accountToAdd.EmailAddress,
            DateOfBirth = accountToAdd.DateOfBirth
        };

        var response = await _accountRepository.AddNewUser(account);
        
        _emailService.SendVerificationMail(response.Id);
        
        return new UserDto{
            Username = response.Username,
            Token = _tokenService.CreateToken(response),
            Id = response.Id
        };
    }

    public async Task<bool> ActivateAccountUsingSingleSignOnToken(Guid accountGuid, string tokenValue)
    {
        var fromBase64 = Convert.FromBase64String(tokenValue);
        
        //todo write this decryption later
        // var decryptSingleSingOnToken = _tokenService.DecryptSingleSingOnToken(fromBase64);

        if (await _singleSignOnTokenRepository.DoesTokenExistAndIsValid(tokenValue) == false)
        {
            _emailService.SendVerificationMail(accountGuid);
            return false;
        }

        await _singleSignOnTokenRepository.ConsumeToken(tokenValue);
        await _accountRepository.UpdateUserStatus(accountGuid, AccountStatus.Verified);

        return true;
    }

    public async Task<AccountInformationDto> UpdateUserStatus(Guid userGuid, AccountStatus status)
    {
        return await _accountRepository.UpdateUserStatus(userGuid, status);
    }

    public async Task<List<AccountInformationDto>> GetAllUsers()
    {
        return await _accountRepository.GetAllUsers();
    }

    public async Task<AccountInformationDto> GetUserById(Guid userGuid)
    {
        return await _accountRepository.GetUserById(userGuid);
    }

}