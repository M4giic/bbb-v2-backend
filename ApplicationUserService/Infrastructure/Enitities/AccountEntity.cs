// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using System.ComponentModel.DataAnnotations;

namespace BBBv2.Infrastructure.Entities;

public class AccountEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Username{ get; set; }
    public byte[] PasswordSalt{ get; set; }
    public byte[] PasswordHash{ get; set; }
        
    [EmailAddress]
    public string EmailAddress{ get; set; }
    
    public AccountStatus AccountStatus { get; set; }

    public DateTime DateOfBirth{ get; set; }
}

public enum AccountStatus
{
    Registered,
    Verified,
    InActive
}