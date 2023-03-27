using ApplicationUserService.Service;
using AutoMapper;
using BBBv2.Infrastructure.DataContext;
using BBBv2.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationUserService.Infrastructure;

public interface ISingleSignOnTokenRepository
{
    //TODO Both Constume token and does.. methods need to fetch token from repo. That is not optimal
    Task<bool> DoesTokenExistAndIsValid(string tokenValue);
    Task<int> ConsumeToken(string tokenValue);
    Task<string> CreateToken(Guid accountGuid);
}

public class SingleSignOnTokenRepository : ISingleSignOnTokenRepository
{
    private readonly DataContext _dataContext;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public SingleSignOnTokenRepository(DataContext dataContext, ITokenService tokenService, IMapper mapper)
    {
        _dataContext = dataContext;
        _tokenService = tokenService;
    }

    
    public async Task<bool> DoesTokenExistAndIsValid(string tokenValue)
    {
        var token = await _dataContext._singleSignOnTokens.SingleAsync(x => x.Token == tokenValue);

        if (token == null) return false;

        if (token.ExpiryDate < DateTime.UtcNow)
        {
            _dataContext._singleSignOnTokens.Remove(token);
            await _dataContext.SaveChangesAsync();
            return false;
        }

        return true;
    }

    public async Task<int> ConsumeToken(string tokenValue)
    {
        var token = await _dataContext._singleSignOnTokens.SingleAsync(x => x.Token == tokenValue);
        _dataContext._singleSignOnTokens.Remove(token);

        return await _dataContext.SaveChangesAsync();
    }

    public async Task<string> CreateToken(Guid accountGuid)
    {
        var tokenValue = _tokenService.CreateSingleSignOnToken();
        var token = new SingleSignOnTokenEntity()
        {
            ExpiryDate = DateTime.UtcNow.AddDays(1),
            AccountGuid = accountGuid,
            Token = tokenValue
        };

        var encryptedKey = _tokenService.EncryptSingleSingOnToken(tokenValue);
        await _dataContext._singleSignOnTokens.AddAsync(token);
        await _dataContext.SaveChangesAsync();

        return encryptedKey;
    }
}