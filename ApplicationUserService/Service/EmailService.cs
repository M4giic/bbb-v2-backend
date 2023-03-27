// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using ApplicationUserService.Infrastructure;

namespace ApplicationUserService.Service;

public interface IEmailService
{
    void SendVerificationMail( Guid accountGuid);
}

public class EmailService : IEmailService
{
    private readonly ISingleSignOnTokenRepository _singleSignOnTokenRepository;

    public EmailService(ISingleSignOnTokenRepository singleSignOnTokenRepository)
    {
        _singleSignOnTokenRepository = singleSignOnTokenRepository;
    }
    
    public async void SendVerificationMail( Guid accountGuid)
    {
        var verifyToken = await _singleSignOnTokenRepository.CreateToken(accountGuid);
        //this mail should be sent by external service
        Console.WriteLine($"Generated verification token: {verifyToken}");
    }
}