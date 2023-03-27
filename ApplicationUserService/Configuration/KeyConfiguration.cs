// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

namespace ApplicationUserService.Configuration;

public class KeyConfiguration
{
    public int SingleSignOnTokenLenght { get; set; }
    public string PasswordKey { get; set; }
    public string EmailTokenKey { get; set; }
}