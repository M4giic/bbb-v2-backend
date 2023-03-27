// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

namespace OperationService.Request.Request.Type;

public class AddNewTypeFamilyRequest
{
    public Guid WalletId { get; set; }
    public string TagFamilyName { get; set; }
    public List<string> Types { get; set; }
}