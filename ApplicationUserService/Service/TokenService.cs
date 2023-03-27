// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApplicationUserService.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ApplicationUserService.Service;

public interface ITokenService
{
    string CreateToken(AccountDto account);
    string CreateSingleSignOnToken();

    string EncryptSingleSingOnToken(string tokenValue);
    string DecryptSingleSingOnToken(byte[] encrypted);
}

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private static Random random = new Random();
    private static int _length;
    private readonly KeyConfiguration _keyConfiguration;
    public TokenService(IConfiguration config)
    {
        _keyConfiguration = config.GetSection("KeyConfiguration").Get<KeyConfiguration>();
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keyConfiguration.PasswordKey));
        _length = _keyConfiguration.SingleSignOnTokenLenght;
    }

    public string CreateToken(AccountDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Username)
        };

        var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMonths(1), //TODO change this value for different env then devel. configParam
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string CreateSingleSignOnToken()
    {
        const string chars = "abcdefghijklmnoprstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, _length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    
    }

    public string EncryptSingleSingOnToken(string tokenValue)
    {
        /*
        // todo write this encryption later
        // key and iv should be calculated from tokenValue hash ? 
        byte[] encrypted;  
        using var aes = new AesManaged();
        aes.Key = Encoding.UTF8.GetBytes(_keyConfiguration.EmailTokenKey);
        
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  
        using(MemoryStream ms = new MemoryStream()) {  
            using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(tokenValue);
                    sw.Write(aes.IV);
                }
                encrypted = ms.ToArray();  
            }  
        }

        return Convert.ToBase64String(encrypted);
        */
        return tokenValue;
    }
    
    public string DecryptSingleSingOnToken(byte[] encrypted)
    {
        string plaintext;
        
        using var aes = new AesManaged();
        
        aes.Key = Encoding.UTF8.GetBytes(_keyConfiguration.EmailTokenKey);
        
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);  
        using(MemoryStream ms = new MemoryStream(encrypted)) {  
            using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) {  
                using(StreamReader reader = new StreamReader(cs))  
                    plaintext = reader.ReadToEnd();  
            }  
        }  
        return plaintext;
    }
}