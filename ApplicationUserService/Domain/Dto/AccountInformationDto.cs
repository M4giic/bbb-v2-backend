// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using BBBv2.Infrastructure.Entities;

namespace ApplicationUserService.Service;

//This dto is used as external API relation to AccountDto
public class AccountInformationDto
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    
    public string EmailAddress { get; init; }

    public DateTime DateOfBirth { get; init; }
    public AccountStatus AccountStatus { get; init; }
    
}