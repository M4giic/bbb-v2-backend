using ApplicationUserService.Service;
using AutoMapper;
using BBBv2.Infrastructure.DataContext;
using BBBv2.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ApplicationUserService.Infrastructure;


internal interface IAccountRepository
{
    Task<bool> DoesUserExistByUsername(string username);
    Task<bool> DoesUserExistByEmail(string email);
    Task<AccountEntity> GetUserByUsername(string Username);
    Task<AccountEntity> GetUserByEmail(string Email);
    Task<AccountDto> AddNewUser(AccountEntity accountToAdd);
    Task<bool> DoesUserExistByGuid(Guid accountGuid);
    Task<AccountInformationDto> UpdateUserStatus(Guid userGuid, AccountStatus status);
    Task<AccountInformationDto> GetUserById(Guid userGuid);
    Task<List<AccountInformationDto>> GetAllUsers();
}

internal class AccountRepository : IAccountRepository
{
    private readonly DataContext _dataContext;
    private IMapper _mapper;

    public AccountRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<bool> DoesUserExistByUsername(string username)
    {
        return await _dataContext._accounts.AnyAsync(x => x.Username.ToLower() == username.ToLower());
    }

    public async Task<bool> DoesUserExistByEmail(string email)
    {
        return await _dataContext._accounts.AnyAsync(x => x.EmailAddress.ToLower() ==  email.ToLower());
    }
    
    public async Task<bool> DoesUserExistByGuid(Guid accountGuid)
    {
        return await _dataContext._accounts.AnyAsync(x => x.Id == accountGuid);
    }

    public async Task<AccountDto> AddNewUser(AccountEntity accountToAdd)
    {
        await _dataContext._accounts.AddAsync(accountToAdd);
        await _dataContext.SaveChangesAsync();
        
        return _mapper.Map<AccountDto>(accountToAdd);
    }

    public async Task<AccountInformationDto> UpdateUserStatus(Guid accountGuid, AccountStatus status)
    {
        var user = await _dataContext._accounts.FindAsync(accountGuid);

        user.AccountStatus = status;

        await _dataContext.SaveChangesAsync();
        
        return _mapper.Map<AccountInformationDto>(user);
    }

    public async Task<AccountInformationDto> GetUserById(Guid userGuid)
    {
        var user = await _dataContext._accounts.FindAsync(userGuid);
        
        return _mapper.Map<AccountInformationDto>(user);
    }

    public async Task<List<AccountInformationDto>> GetAllUsers()
    {
        var dupa = await _dataContext._accounts.ToListAsync();
        return _mapper.Map<List<AccountInformationDto>>(dupa);
    }

    public async Task<AccountEntity> GetUserByUsername(string Username)
    {
        return await _dataContext._accounts.FirstOrDefaultAsync(x => x.Username == Username);
    }
    public async Task<AccountEntity> GetUserByEmail(string Email)
    {
        return await _dataContext._accounts.FirstOrDefaultAsync(x => x.EmailAddress == Email);
    }
}