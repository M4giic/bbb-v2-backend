// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

namespace OperationService.Request.Request.Wallet;

public class AddWalletRequest
{
    public string WalletName { get; set; }
    public Guid OwnerUserGuid { get; set; }
}