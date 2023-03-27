// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using System.Text.Json.Serialization;

namespace OperationService.Request.Request.Wallet;

public class UpdateWalletRequest
{
    public string WalletName { get; set; }
    
    [JsonIgnore]
    public Guid Id { get; set; }
}